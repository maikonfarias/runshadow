Shader "Custom/SkyShader"
{
	Subshader
	{
		BindChannels
		{
			Bind "vertex", 
			vertex Bind "color", 
			color
		}
		Pass{}
	}
}