using System;
using Mogre;

namespace RenderingEngine.Engine
{
    class PolygonRayCast
    {
        private RaySceneQuery mRaySceneQuery;

        public PolygonRayCast()
        {
            mRaySceneQuery = Engine.Instance.SceneManager.CreateRayQuery(new Ray(), SceneManager.WORLD_GEOMETRY_TYPE_MASK);
            mRaySceneQuery.SetSortByDistance(true);
        }

        // raycast from a point in to the scene.
        // returns success or failure.
        // on success the point is returned in the result.
        public bool RaycastFromPoint(Vector3 point, Vector3 normal, ref Vector3 result,ref Vector3 resNormal)
        {
            // create the ray to test
            Ray ray = new Ray(point, normal);

            // check we are initialised
            if (mRaySceneQuery != null)
            {
                // create a query object
                mRaySceneQuery.Ray = ray;

                // execute the query, returns a vector of hits
                RaySceneQueryResult rayresult = mRaySceneQuery.Execute();
                if (rayresult.Count <= 0)
                {
                    // raycast did not hit an objects bounding box
                    return false;
                }
            }
            else
            {
                return false;
            }

            // at this point we have raycast to a series of different objects bounding boxes.
            // we need to test these different objects to see which is the first polygon hit.
            // there are some minor optimizations (distance based) that mean we wont have to
            // check all of the objects most of the time, but the worst case scenario is that
            // we need to test every triangle of every object.
            float closest_distance = -1.0f;
            Vector3 closest_result = Vector3.ZERO;
            Vector3 vNormal = Vector3.ZERO;
            RaySceneQueryResult query_result = mRaySceneQuery.GetLastResults();

            foreach (RaySceneQueryResultEntry this_result in query_result)
            {
                // stop checking if we have found a raycast hit that is closer
                // than all remaining entities
                if ((closest_distance >= 0.0f) &&
                    (closest_distance < this_result.distance))
                {
                    break;
                }

                // only check this result if its a hit against an entity
                if ((this_result.movable != null) &&
                    (this_result.movable.MovableType == "Entity"))
                {
                    // get the entity to check
                    Entity pentity = (Entity)this_result.movable;

                    // mesh data to retrieve 
                    ulong vertex_count = 0;
                    ulong index_count = 0;
                    Vector3[] vertices = new Vector3[0];
                    UInt64[] indices = new UInt64[0];

                    // get the mesh information
                    GetMeshInformation(pentity.GetMesh(),ref vertex_count, ref vertices, ref index_count, ref indices,
                        pentity.ParentNode._getDerivedPosition(),    // WorldPosition
                        pentity.ParentNode._getDerivedOrientation(), // WorldOrientation
                        pentity.ParentNode.GetScale());

                    int ncf = -1; // new_closest_found

                    // test for hitting individual triangles on the mesh
                    for (int i = 0; i < (int)index_count; i += 3)
                    {
                        // check for a hit against this triangle
                        Pair<bool, float> hit = Mogre.Math.Intersects(ray, vertices[indices[i]],
                            vertices[indices[i + 1]], vertices[indices[i + 2]], true, false);

                        // if it was a hit check if its the closest
                        if (hit.first)
                        {
                            if ((closest_distance < 0.0f) ||
                                (hit.second < closest_distance))
                            {
                                // this is the closest so far, save it off
                                closest_distance = hit.second;
                                ncf = i;
                            }
                        }
                    }

                    if (ncf > -1)
                    {
                        closest_result = ray.GetPoint(closest_distance);
                        // if you don't need the normal, comment this out; you'll save some CPU cycles.
                        Vector3 v1 = vertices[indices[ncf]] - vertices[indices[ncf + 1]];
                        Vector3 v2 = vertices[indices[ncf + 2]] - vertices[indices[ncf + 1]];
                        vNormal = v1.CrossProduct(v2);
                    }

                    // free the verticies and indicies memory
                    vertices = null;
                    indices = null;
                }
            }

            // if we found a new closest raycast for this object, update the
            // closest_result before moving on to the next object.
            if (closest_distance >= 0.0f)
            {
                result = new Vector3(closest_result.x, closest_result.y, closest_result.z);
                resNormal = vNormal / vNormal.Normalise();

                /*
                // this visualizes the 'result' position 
                if (!sceneMgr.HasSceneNode("marker"))
                {
                    SceneNode node = sceneMgr.CreateSceneNode("marker");
                    Entity ent = sceneMgr.CreateEntity("marker", "Cube.mesh");
                    node.AttachObject(ent);
                    node.Position = result;
                    node.Scale(0.25f, 0.25f, 0.25f);
                    sceneMgr.RootSceneNode.AddChild(node);
                }
                else
                {
                    sceneMgr.GetSceneNode("marker").Position = result;
                }
                */

                // raycast success
                return true;
            }
            else
            {
                // raycast failed
                return false;
            }
        }

        // Get the mesh information for the given mesh.
        // Code found in Wiki: www.ogre3d.org/wiki/index.php/RetrieveVertexData
        public unsafe void GetMeshInformation(MeshPtr mesh,
            ref ulong vertex_count,
            ref Vector3[] vertices,
            ref ulong index_count,
            ref UInt64[] indices,
            Vector3 position,
            Quaternion orientation,
            Vector3 scale)
        {
            bool addedShared = false;
            ulong currentOffset = 0;
            ulong sharedOffset = 0;
            ulong nextOffset = 0;
            ulong indexOffset = 0;

            vertex_count = index_count = 0;

            // Calculate how many vertices and indices we're going to need
            for (ushort i = 0; i < mesh.NumSubMeshes; ++i)
            {
                SubMesh submesh = mesh.GetSubMesh(i);

                // We only need to add the shared vertices once
                if (submesh.useSharedVertices)
                {
                    if (!addedShared)
                    {
                        vertex_count += mesh.sharedVertexData.vertexCount;
                        addedShared = true;
                    }
                }
                else
                {
                    vertex_count += submesh.vertexData.vertexCount;
                }

                // Add the indices
                index_count += submesh.indexData.indexCount;
            }

            // Allocate space for the vertices and indices
            vertices = new Vector3[vertex_count];
            indices = new UInt64[index_count];
            addedShared = false;

            // Run through the submeshes again, adding the data into the arrays
            for (ushort i = 0; i < mesh.NumSubMeshes; ++i)
            {
                SubMesh submesh = mesh.GetSubMesh(i);
                VertexData vertexData = submesh.useSharedVertices ? mesh.sharedVertexData : submesh.vertexData;

                if (!submesh.useSharedVertices || (submesh.useSharedVertices && !addedShared))
                {
                    if (submesh.useSharedVertices)
                    {
                        addedShared = true;
                        sharedOffset = currentOffset;
                    }

                    VertexElement posElem =
                        vertexData.vertexDeclaration.FindElementBySemantic(VertexElementSemantic.VES_POSITION);
                    HardwareVertexBufferSharedPtr vbuf =
                        vertexData.vertexBufferBinding.GetBuffer(posElem.Source);

                    byte* vertex = (byte*)vbuf.Lock(HardwareBuffer.LockOptions.HBL_READ_ONLY);
                    float* pReal;

                    // There is _no_ baseVertexPointerToElement() which takes an Ogre::Real or a double
                    //  as second argument. So make it float, to avoid trouble when Ogre::Real will
                    //  be comiled/typedefed as double:
                    //      Ogre::Real* pReal;
                    for (ulong j = 0; j < vertexData.vertexCount; ++j, vertex += vbuf.VertexSize)
                    {
                        posElem.BaseVertexPointerToElement(vertex, &pReal);
                        Vector3 pt = new Vector3(pReal[0], pReal[1], pReal[2]);
                        vertices[currentOffset + j] = (orientation * (pt * scale)) + position;
                    }
                    // |!| Important: VertexBuffer Unlock() + Dispose() avoids memory corruption
                    vbuf.Unlock();
                    vbuf.Dispose();
                    nextOffset += vertexData.vertexCount;
                }

                IndexData indexData = submesh.indexData;
                ulong numTris = indexData.indexCount / 3;
                HardwareIndexBufferSharedPtr ibuf = indexData.indexBuffer;

                // UNPORTED line of C++ code (because ibuf.IsNull() doesn't exist in C#)
                // if( ibuf.isNull() ) continue
                // need to check if index buffer is valid (which will be not if the mesh doesn't have triangles like a pointcloud)

                bool use32Bitindexes = (ibuf.Type == HardwareIndexBuffer.IndexType.IT_32BIT);

                uint* pLong = (uint*)ibuf.Lock(HardwareBuffer.LockOptions.HBL_READ_ONLY);
                ushort* pShort = (ushort*)pLong;
                ulong offset = submesh.useSharedVertices ? sharedOffset : currentOffset;
                if (use32Bitindexes)
                {
                    for (ulong k = 0; k < indexData.indexCount; ++k)
                    {
                        indices[indexOffset++] = (UInt64)pLong[k] + (UInt64)offset;
                    }
                }
                else
                {
                    for (ulong k = 0; k < indexData.indexCount; ++k)
                    {
                        indices[indexOffset++] = (UInt64)pShort[k] + (UInt64)offset;
                    }
                }
                // |!| Important: IndexBuffer Unlock() + Dispose() avoids memory corruption
                ibuf.Unlock();
                ibuf.Dispose();
                currentOffset = nextOffset;
            }

            // |!| Important: MeshPtr Dispose() avoids memory corruption
            mesh.Dispose(); // This dispose the MeshPtr, not the Mesh

        }
    }
}
