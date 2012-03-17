using System.Collections.Generic;
using System.Globalization;
using System.IO;
using OpenTK;
namespace Soltys.ProgrammerBot.OpenGLEngine
{
    public static class ObjMeshLoader
    {
        // static class
        public static ObjMesh Load(string fileName)
        {
            ObjMesh mesh = null;
            using (var streamReader = new StreamReader(fileName))
            {
                mesh = load(streamReader);
            }
            return mesh;
        }

        const char SPLIT_CHARACTERS = ' ';

        static List<Vector3> vertices;
        static List<Vector3> normals;
        static List<Vector2> texCoords;
        static Dictionary<ObjVertex, int> objVerticesIndexDictionary;
        static List<ObjVertex> objVertices;
        static List<ObjTriangle> objTriangles;
        static List<ObjQuad> objQuads;

        static ObjMesh load(TextReader textReader)
        {
            vertices = new List<Vector3>();
            normals = new List<Vector3>();
            texCoords = new List<Vector2>();
            objVerticesIndexDictionary = new Dictionary<ObjVertex, int>();
            objVertices = new List<ObjVertex>();
            objTriangles = new List<ObjTriangle>();
            objQuads = new List<ObjQuad>();

            ObjMesh mesh = new ObjMesh();

            string line;
            while ((line = textReader.ReadLine()) != null)
            {
                line = line.Trim(SPLIT_CHARACTERS);
                line = line.Replace("  ", " ");

                string[] parameters = line.Split(SPLIT_CHARACTERS);

                switch (parameters[0])
                {
                    case "p": // Point
                        break;

                    case "v": // Vertex
                        float x = float.Parse(parameters[1], CultureInfo.InvariantCulture);
                        float y = float.Parse(parameters[2], CultureInfo.InvariantCulture);
                        float z = float.Parse(parameters[3], CultureInfo.InvariantCulture);
                        vertices.Add(new Vector3(x, y, z));
                        break;

                    case "vt": // TexCoord
                        float u = float.Parse(parameters[1], CultureInfo.InvariantCulture);
                        float v = float.Parse(parameters[2], CultureInfo.InvariantCulture);
                        texCoords.Add(new Vector2(u, v));
                        break;

                    case "vn": // Normal
                        float nx = float.Parse(parameters[1], CultureInfo.InvariantCulture);
                        float ny = float.Parse(parameters[2], CultureInfo.InvariantCulture);
                        float nz = float.Parse(parameters[3], CultureInfo.InvariantCulture);
                        normals.Add(new Vector3(nx, ny, nz));
                        break;

                    case "f":
                        switch (parameters.Length)
                        {
                            case 4:
                                ObjTriangle objTriangle = new ObjTriangle();
                                objTriangle.Index0 = parseFaceParameter(parameters[1]);
                                objTriangle.Index1 = parseFaceParameter(parameters[2]);
                                objTriangle.Index2 = parseFaceParameter(parameters[3]);
                                objTriangles.Add(objTriangle);
                                break;

                            case 5:
                                ObjQuad objQuad = new ObjQuad();
                                objQuad.Index0 = parseFaceParameter(parameters[1]);
                                objQuad.Index1 = parseFaceParameter(parameters[2]);
                                objQuad.Index2 = parseFaceParameter(parameters[3]);
                                objQuad.Index3 = parseFaceParameter(parameters[4]);
                                objQuads.Add(objQuad);
                                break;
                        }
                        break;
                }
            }

            mesh.Vertices = objVertices.ToArray();
            mesh.Triangles = objTriangles.ToArray();
            mesh.Quads = objQuads.ToArray();

            return mesh;
        }

        const char FACE_PARAMATER_SPLITTER = '/';
        static int parseFaceParameter(string faceParameter)
        {
            Vector3 vertex = new Vector3();
            Vector2 texCoord = new Vector2();
            Vector3 normal = new Vector3();

            string[] parameters = faceParameter.Split(FACE_PARAMATER_SPLITTER);

            int vertexIndex = int.Parse(parameters[0], CultureInfo.InvariantCulture);
            if (vertexIndex < 0) vertexIndex = vertices.Count + vertexIndex;
            else vertexIndex = vertexIndex - 1;
            vertex = vertices[vertexIndex];

            if (parameters.Length > 1)
            {
                int texCoordIndex = int.Parse(parameters[1], CultureInfo.InvariantCulture);
                if (texCoordIndex < 0) texCoordIndex = texCoords.Count + texCoordIndex;
                else texCoordIndex = texCoordIndex - 1;
                texCoord = texCoords[texCoordIndex];
            }

            if (parameters.Length > 2)
            {
                int normalIndex = int.Parse(parameters[2], CultureInfo.InvariantCulture);
                if (normalIndex < 0) normalIndex = normals.Count + normalIndex;
                else normalIndex = normalIndex - 1;
                normal = normals[normalIndex];
            }

            return findOrAddObjVertex(ref vertex, ref texCoord, ref normal);
        }

        static int findOrAddObjVertex(ref Vector3 vertex, ref Vector2 texCoord, ref Vector3 normal)
        {
            ObjVertex newObjVertex = new ObjVertex();
            newObjVertex.Vertex = vertex;
            newObjVertex.TexCoord = texCoord;
            newObjVertex.Normal = normal;

            int index;
            if (objVerticesIndexDictionary.TryGetValue(newObjVertex, out index))
            {
                return index;
            }
            else
            {
                objVertices.Add(newObjVertex);
                objVerticesIndexDictionary[newObjVertex] = objVertices.Count - 1;
                return objVertices.Count - 1;
            }
        }
    }
}