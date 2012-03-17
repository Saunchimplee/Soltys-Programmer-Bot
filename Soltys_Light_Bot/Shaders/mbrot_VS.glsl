varying vec3 position;
void main()
{
	position = vec3(gl_MultiTexCoord0 - 0.5) * 5.0;
	gl_Position = ftransform();
}