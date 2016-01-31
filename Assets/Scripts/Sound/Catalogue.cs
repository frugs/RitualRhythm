public class Catalogue
{
	private const string MUSIC = "event:/Music/Beat";

	public const string VOX = "event:/SFX/Vox/";
	public const string IMPACT = "event:/SFX/Impacts/";
	
	public class Character {
		
		public const string A = "A/";
		public const string D = "D/";
		public const string O = "O/";
		public const string H = "H/";
		public const string M = "M/";
	}

	public class Type {

		public const string BLOCK = "Block";
		public const string HURT = "Hurt";
		public const string STRIKE = "Strike";
		public const string DEATH = "Death";

		public const string SMALL = "Small";
		public const string BIG = "Big";
		public const string MISS = "Miss";
	}

	public static string getMusic() {
		return MUSIC;
	}

	public static string getVox(string character, string type) {
		return VOX + character + type;
	}

	public static string getImpact(string type) {
		return IMPACT + type;
	}
}

