#version 110
void main(void)
{
  gl_Position = ftransform(); // gl_ModelViewProjectionMatrix * gl_Vertex;
}