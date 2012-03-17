using OpenTK.Graphics.OpenGL;
namespace Soltys.ProgrammerBot
{
    class StandardLighting : ILighting
    {
        public void TurnOnLights()
        {
            const float angle = 0.1f;
            const float lowBorder = 0.0f;
            const float upperBorder = 8.0f;
            const float lightHeigth = 2.0f;
            var lightPosition = new float[] { lowBorder, lightHeigth, -lowBorder, 1.0f };
            var lightSpot = new float[] { angle, -1.0f, -angle, 1.0f };
            var spotCutoff = 90;
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
            GL.Light(LightName.Light0, LightParameter.SpotDirection, lightSpot);
            GL.Light(LightName.Light0, LightParameter.SpotCutoff, spotCutoff);

            var lightPosition1 = new float[] { lowBorder, lightHeigth, -upperBorder, 1.0f };
            var lightSpot1 = new float[] { angle, -1.0f, angle, 1.0f };
            GL.Light(LightName.Light1, LightParameter.Position, lightPosition1);
            GL.Light(LightName.Light1, LightParameter.SpotDirection, lightSpot1);
            GL.Light(LightName.Light1, LightParameter.SpotCutoff, spotCutoff);

            var lightPosition2 = new float[] { upperBorder, lightHeigth, -upperBorder, 1.0f };
            var lightSpot2 = new float[] { -angle, -1.0f, angle, 1.0f };
            GL.Light(LightName.Light2, LightParameter.Position, lightPosition2);
            GL.Light(LightName.Light2, LightParameter.SpotDirection, lightSpot2);
            GL.Light(LightName.Light2, LightParameter.SpotCutoff, spotCutoff);

            var lightPosition3 = new float[] { upperBorder, lightHeigth, lowBorder, 1.0f };
            var lightSpot3 = new float[] { -angle, -1.0f, -angle, 1.0f };
            GL.Light(LightName.Light3, LightParameter.Position, lightPosition3);
            GL.Light(LightName.Light3, LightParameter.SpotDirection, lightSpot3);
            GL.Light(LightName.Light3, LightParameter.SpotCutoff, spotCutoff);


            // włączenie światła GL_LIGHT0 z parametrami domyślnymi
            //set the light specular to white
            var whiteSpecularLight = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            //set the light ambient to black
            var blackAmbientLight = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
            //set the diffuse light to white
            var whiteDiffuseLight = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };

            setLightColor(LightName.Light0, whiteSpecularLight, blackAmbientLight, whiteDiffuseLight);
            setLightColor(LightName.Light1, whiteSpecularLight, blackAmbientLight, whiteDiffuseLight);
            setLightColor(LightName.Light2, whiteSpecularLight, blackAmbientLight, whiteDiffuseLight);
            setLightColor(LightName.Light3, whiteSpecularLight, blackAmbientLight, whiteDiffuseLight);
             enableLights();
        }
        private void enableLights()
        {
            GL.Enable(EnableCap.Light0);
           // GL.Enable(EnableCap.Light1);
           // GL.Enable(EnableCap.Light2);
           // GL.Enable(EnableCap.Light3);
        }

        private void setLightColor(LightName lightName, float[] whiteSpecularLight, float[] blackAmbientLight, float[] whiteDiffuseLight)
        {
            GL.Light(lightName, LightParameter.Specular, whiteSpecularLight);
            GL.Light(lightName, LightParameter.Ambient, blackAmbientLight);
            GL.Light(lightName, LightParameter.Diffuse, whiteDiffuseLight);

        }


        public void TurnOffLights()
        {
            GL.Disable(EnableCap.Light0);
            GL.Disable(EnableCap.Light1);
            GL.Disable(EnableCap.Light2);
            GL.Disable(EnableCap.Light3);
        }
    }
}
