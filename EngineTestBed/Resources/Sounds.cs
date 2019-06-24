using ChaiFoxes.FMODAudio;


namespace Resources
{
	public static class Sounds
	{

		public static Sound Checkpoint;

		public static void Load()
		{
			Checkpoint = AudioMgr.LoadSound("Checkpoint.wav");
		}

		static Sound Load3DSound(string path, float minDistance, float maxDistance)
		{
			var sound = AudioMgr.LoadSound(path);
			sound.Is3D = true;
			sound.MinDistance3D = minDistance;
			sound.MaxDistance3D = maxDistance;

			return sound;
		}

		public static void Unload()
		{
		}

	}
}
