﻿using System;
using System.Diagnostics;
using Mogre;

namespace RenderingEngine.Helpers
{
    public class CollisionTools
    {
        #region Fields

        private SceneManager sceneMgr =  Engine.Engine.Instance.SceneManager;

        #endregion Fields

        private static CollisionTools mInstance;

        public static CollisionTools Instance
        {
            get { return mInstance ?? (mInstance = new CollisionTools()); }
        }

        #region Constructors

        private CollisionTools()
        {
            this.HeightAdjust = 0.0f;
        }

        #endregion Constructors

        #region Properties

        #region Public Properties

        public float HeightAdjust
        {
            get;
            set;
        }

        #endregion Public Properties

        #endregion Properties

        #region Methods

        #region Public Methods

        public void CalculateY(SceneNode n, bool doTerrainCheck, bool doGridCheck, float gridWidth, uint queryMask)
        {
            Vector3 pos = n.Position;

            float x = pos.x;
            float z = pos.z;
            float y = pos.y;

            float terrY = 0, colY = 0, colY2 = 0;

            RaycastResult rr = this.RaycastFromPoint(new Vector3(x, y, z), Vector3.NEGATIVE_UNIT_Y, queryMask);
            if (rr != null)
            {
                if (rr.Target != null)
                {
                    colY = rr.Position.y;
                }
                else
                {
                    colY = -99999;
                }
            }

            // if doGridCheck is on, repeat not to fall through small holes for example when crossing a hangbridge
            if (doGridCheck)
            {
                RaycastResult rr2 = this.RaycastFromPoint(new Vector3(x, y, z) + (n.Orientation * new Vector3(0, 0, gridWidth)), Vector3.NEGATIVE_UNIT_Y, queryMask);
                if (rr2 != null)
                {
                    if (rr2.Target != null)
                    {
                        colY2 = rr2.Position.y;
                    }
                    else
                    {
                        colY2 = -99999;
                    }
                }

                if (colY < colY2)
                {
                    colY = colY2;
                }
            }

            // set the parameter to false if you are not using ETM or TSM
            if (doTerrainCheck)
            {
                terrY = this.GetTSMHeightAt(x, z);

                if (terrY < colY)
                {
                    n.Position = new Vector3(x, colY + this.HeightAdjust, z);
                }
                else
                {
                    n.Position = new Vector3(x, terrY + this.HeightAdjust, z);
                }
            }
            else
            {
                if (!doTerrainCheck && colY == -99999)
                {
                    colY = y;
                }

                n.Position = new Vector3(x, colY + this.HeightAdjust, z);
            }
        }

        public bool CollidesWithEntity(Vector3 fromPoint, Vector3 toPoint, float collisionRadius, float rayHeightLevel, uint queryMask)
        {
            Vector3 fromPointAdj = new Vector3(fromPoint.x, fromPoint.y + rayHeightLevel, fromPoint.z);
            Vector3 toPointAdj = new Vector3(toPoint.x, toPoint.y + rayHeightLevel, toPoint.z);
            Vector3 normal = toPointAdj - fromPointAdj;
            float distToDest = normal.Normalise();

            RaycastResult rr = this.RaycastFromPoint(fromPointAdj, normal, queryMask);
            if (rr != null)
            {
                rr.Distance -= collisionRadius;
                return rr.Distance <= distToDest;
            }
            else
            {
                return false;
            }
        }

        public float GetTSMHeightAt(float x, float z)
        {
            float y = 0.0f;

            Ray updateRay = new Ray();

            updateRay.Origin = new Vector3(x, 9999, z);
            updateRay.Direction = Vector3.NEGATIVE_UNIT_Y;

            using (RaySceneQuery tsmRaySceneQuery = this.sceneMgr.CreateRayQuery(updateRay))
            {
                using (RaySceneQueryResult qryResult = tsmRaySceneQuery.Execute())
                {
                    RaySceneQueryResult.Iterator i = qryResult.Begin();
                    if (i != qryResult.End() && i.Value.worldFragment != null)
                    {
                        y = i.Value.worldFragment.singleIntersection.y;
                    }
                }

                this.sceneMgr.DestroyQuery(tsmRaySceneQuery);
            }

            return y;
        }

        public RaycastResult Raycast(Ray ray, uint queryMask)
        {
            RaycastResult rr = new RaycastResult();

            RaySceneQuery raySceneQuery = this.sceneMgr.CreateRayQuery(new Ray());
            raySceneQuery.SetSortByDistance(true);

            // check we are initialised
            if (raySceneQuery != null)
            {
                // create a query object
                raySceneQuery.Ray = ray;
                raySceneQuery.SetSortByDistance(true);
                raySceneQuery.QueryMask = queryMask;

                using (RaySceneQueryResult queryResult = raySceneQuery.Execute())
                {
                    // execute the query, returns a vector of hits
                    if (queryResult.Count <= 0)
                    {
                        // raycast did not hit an objects bounding box
                        this.sceneMgr.DestroyQuery(raySceneQuery);
                        raySceneQuery.Dispose();
                        return null;
                    }

                    // at this point we have raycast to a series of different objects bounding boxes.
                    // we need to test these different objects to see which is the first polygon hit.
                    // there are some minor optimizations (distance based) that mean we wont have to
                    // check all of the objects most of the time, but the worst case scenario is that
                    // we need to test every triangle of every object.
                    // Ogre::Real closest_distance = -1.0f;
                    rr.Distance = -1.0f;
                    Vector3 closestResult = Vector3.ZERO;

                    for (int qridx = 0; qridx < queryResult.Count; qridx++)
                    {
                        // stop checking if we have found a raycast hit that is closer
                        // than all remaining entities
                        if (rr.Distance >= 0.0f && rr.Distance < queryResult[qridx].distance)
                        {
                            break;
                        }

                        // only check this result if its a hit against an entity
                        if (queryResult[qridx].movable != null
                            && queryResult[qridx].movable.MovableType == "Entity")
                        {
                            // get the entity to check
                            Entity entity = (Entity)queryResult[qridx].movable;

                            // mesh data to retrieve
                            Vector3[] vertices;
                            int[] indices;
                            RenderOperation.OperationTypes opType;

                            // get the mesh information
                            using (MeshPtr mesh = entity.GetMesh())
                            {
                                opType = mesh.GetSubMesh(0).operationType;

                                Debug.Assert(CheckSubMeshOpType(mesh, opType));

                                GetMeshInformation(
                                    mesh,
                                    out vertices,
                                    out indices,
                                    entity.ParentNode._getDerivedPosition(),
                                    entity.ParentNode._getDerivedOrientation(),
                                    entity.ParentNode._getDerivedScale());
                            }

                            int vertexCount = vertices.Length;
                            int indexCount = indices.Length;

                            // test for hitting individual triangles on the mesh
                            bool newClosestFound = false;
                            Pair<bool, float> hit;
                            switch (opType)
                            {
                                case RenderOperation.OperationTypes.OT_TRIANGLE_LIST:
                                    for (int i = 0; i < indexCount; i += 3)
                                    {
                                        hit = Mogre.Math.Intersects(ray, vertices[indices[i]], vertices[indices[i + 1]], vertices[indices[i + 2]], true, false);

                                        if (CheckDistance(rr, hit))
                                        {
                                            newClosestFound = true;
                                        }
                                    }
                                    break;
                                case RenderOperation.OperationTypes.OT_TRIANGLE_STRIP:
                                    for (int i = 0; i < indexCount - 2; i++)
                                    {
                                        hit = Mogre.Math.Intersects(ray, vertices[indices[i]], vertices[indices[i + 1]], vertices[indices[i + 2]], true, true);

                                        if (CheckDistance(rr, hit))
                                        {
                                            newClosestFound = true;
                                        }
                                    }
                                    break;
                                case RenderOperation.OperationTypes.OT_TRIANGLE_FAN:
                                    for (int i = 0; i < indexCount - 2; i++)
                                    {
                                        hit = Mogre.Math.Intersects(ray, vertices[indices[0]], vertices[indices[i + 1]], vertices[indices[i + 2]], true, true);

                                        if (CheckDistance(rr, hit))
                                        {
                                            newClosestFound = true;
                                        }
                                    }
                                    break;
                                default:
                                    throw new Exception("invalid operation type");
                            }

                            // if we found a new closest raycast for this object, update the
                            // closest_result before moving on to the next object.
                            if (newClosestFound)
                            {
                                rr.Target = entity;
                                closestResult = ray.GetPoint(rr.Distance);
                            }
                        }
                    }

                    this.sceneMgr.DestroyQuery(raySceneQuery);
                    raySceneQuery.Dispose();

                    // return the result
                    if (rr.Distance >= 0.0f)
                    {
                        // raycast success
                        rr.Position = closestResult;
                        return rr;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public RaycastResult RaycastFromCamera(RenderWindow window, Camera camera, Vector2 point, uint queryMask)
        {
            // Create the ray to test
            float tx = point.x / window.Width;
            float ty = point.y / window.Height;
            Ray ray = camera.GetCameraToViewportRay(tx, ty);

            return this.Raycast(ray, queryMask);
        }

        public RaycastResult RaycastFromPoint(Vector3 origin, Vector3 direction, uint queryMask)
        {
            // create the ray to test
            Ray ray = new Ray();
            ray.Origin = origin;
            ray.Direction = direction;

            return this.Raycast(ray, queryMask);
        }

        #endregion Public Methods

        #region Private Static Methods

        private static bool CheckDistance(RaycastResult rr, Pair<bool, float> hit)
        {
            // if it was a hit check if its the closest
            if (hit.first)
            {
                if ((rr.Distance < 0.0f) ||
                    (hit.second < rr.Distance))
                {
                    // this is the closest so far, save it off
                    rr.Distance = hit.second;
                    return true;
                }
            }

            return false;
        }

        // Get the mesh information for the given mesh.
        // Code found on this forum link: http://www.ogre3d.org/wiki/index.php/RetrieveVertexData
        private static unsafe void GetMeshInformation(MeshPtr mesh, out Vector3[] vertices, out int[] indices, Vector3 position, Quaternion orient, Vector3 scale)
        {
            bool addedShared = false;
            int currentOffset = 0, sharedOffset = 0, nextOffset = 0, indexOffset = 0;

            int vertexCount = 0, indexCount = 0;

            // Calculate how many vertices and indices we're going to need
            for (ushort i = 0; i < mesh.NumSubMeshes; ++i)
            {
                SubMesh submesh = mesh.GetSubMesh(i);

                // We only need to add the shared vertices once
                if (submesh.useSharedVertices)
                {
                    if (!addedShared)
                    {
                        vertexCount += (int)mesh.sharedVertexData.vertexCount;
                        addedShared = true;
                    }
                }
                else
                {
                    vertexCount += (int)submesh.vertexData.vertexCount;
                }

                // Add the indices
                indexCount += (int)submesh.indexData.indexCount;
            }

            // Allocate space for the vertices and indices
            vertices = new Vector3[vertexCount];
            indices = new int[indexCount];

            addedShared = false;

            // Run through the submeshes again, adding the data into the arrays
            for (ushort i = 0; i < mesh.NumSubMeshes; ++i)
            {
                SubMesh submesh = mesh.GetSubMesh(i);

                VertexData vertexData = submesh.useSharedVertices ? mesh.sharedVertexData : submesh.vertexData;

                if ((!submesh.useSharedVertices) || (submesh.useSharedVertices && !addedShared))
                {
                    if (submesh.useSharedVertices)
                    {
                        addedShared = true;
                        sharedOffset = currentOffset;
                    }

                    VertexElement posElem = vertexData.vertexDeclaration.FindElementBySemantic(VertexElementSemantic.VES_POSITION);
                    System.Diagnostics.Debug.Assert(posElem.Type == VertexElementType.VET_FLOAT3);

                    using (HardwareVertexBufferSharedPtr vbuf = vertexData.vertexBufferBinding.GetBuffer(posElem.Source))
                    {
                        byte* vertex = (byte*)vbuf.Lock(HardwareBuffer.LockOptions.HBL_READ_ONLY);
                        float* preal;

                        for (uint j = 0; j < vertexData.vertexCount; ++j, vertex += vbuf.VertexSize)
                        {
                            posElem.BaseVertexPointerToElement(vertex, &preal);
                            Vector3 pt = new Vector3(preal[0], preal[1], preal[2]);

                            vertices[currentOffset + j] = (orient * (pt * scale)) + position;
                        }

                        vbuf.Unlock();
                    }

                    nextOffset += (int)vertexData.vertexCount;
                }

                IndexData indexData = submesh.indexData;

                using (HardwareIndexBufferSharedPtr ibuf = indexData.indexBuffer)
                {
                    bool use32bitindexes = ibuf.Type == HardwareIndexBuffer.IndexType.IT_32BIT;

                    int* plong = (int*)ibuf.Lock(HardwareBuffer.LockOptions.HBL_READ_ONLY);
                    ushort* pshort = (ushort*)plong;
                    int offset = submesh.useSharedVertices ? sharedOffset : currentOffset;

                    if (use32bitindexes)
                    {
                        for (uint k = 0; k < indexData.indexCount; ++k)
                        {
                            indices[indexOffset++] = plong[k] + offset;
                        }
                    }
                    else
                    {
                        for (uint k = 0; k < indexData.indexCount; ++k)
                        {
                            indices[indexOffset++] = pshort[k] + offset;
                        }
                    }

                    ibuf.Unlock();
                }

                currentOffset = nextOffset;
            }
        }

        #endregion Private Static Methods

        #region Private Methods

        private bool CheckSubMeshOpType(MeshPtr mesh, RenderOperation.OperationTypes opType)
        {
            for (ushort i = 0; i < mesh.NumSubMeshes; i++)
            {
                if (mesh.GetSubMesh(i).operationType != opType)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion Private Methods

        #endregion Methods

        #region Nested Types

        public class RaycastResult
        {
            #region Properties

            #region Public Properties

            public float Distance
            {
                get;
                set;
            }

            public Vector3 Position
            {
                get;
                set;
            }

            public Entity Target
            {
                get;
                set;
            }

            #endregion Public Properties

            #endregion Properties
        }

        #endregion Nested Types
    }
}