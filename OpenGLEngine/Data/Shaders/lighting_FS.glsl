#version 110
/* lighting.frag - per fragment lighting */


// switch between vertex and fragment lighting.
uniform bool disablePerFragmentLighting;

// use toon sahding
uniform bool useToonShading;

// wether the eye is located in the origin (true)
// or at (0,0, +infinity) (false)
uniform bool eyeAtOrigin;


// input
varying vec3 normal; // fragment normal in eye space.
varying vec3 position; // fragment position in eye space.


//
// computes light of a single light source.
//
// N normalized vertex normal in eye space.
// V vertex position in eye space.
//
vec4 lightSource( vec3 N, vec3 V, gl_LightSourceParameters light )
{
	vec3  H;
	float d = length( light.position.xyz - V );
	vec3  L = normalize( light.position.xyz - V );

	if( eyeAtOrigin )
	{
		// eye at (0,0,0)
		H = normalize( L - V.xyz );
	}
	else
	{
		// eye at (0,0,infinity)
		H = normalize( L + vec3( 0.0, 0.0, 1.0 ) );
	}

	float NdotL = max( 0.0, dot( N,L ) );
	float NdotH = max( 0.0, dot( N,H ) );

	float Idiff = NdotL;
	float Ispec = pow( NdotH, gl_FrontMaterial.shininess );

	return 
		gl_FrontMaterial.ambient  * light.ambient +
		gl_FrontMaterial.diffuse  * light.diffuse  * Idiff +
		gl_FrontMaterial.specular * light.specular * Ispec;
}


//
// computes light of all light sources and the scene.
//
vec4 lighting( int i )
{
	// normal might be damaged by linear interpolation.
	vec3 N = normalize( normal );

	return
		gl_FrontMaterial.emission +
		gl_FrontMaterial.ambient * gl_LightModel.ambient +
		lightSource( N, position, gl_LightSource[i] );
}



//
// entry point
//
void main( void )
{
	// choose the lighting method.
	if( disablePerFragmentLighting )
	{
		gl_FragColor = gl_Color;
	}
	else
	{		
		gl_FragColor = lighting(0);
		gl_FragColor += lighting(1);
		gl_FragColor += lighting(2);
		gl_FragColor += lighting(3);
	}
}

