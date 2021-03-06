// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using CivOne.Interfaces;

namespace CivOne.Civilizations
{
	internal class Chinese : ICivilization
	{
		public int Id
		{
			get
			{
				return 12;
			}
		}

		public string Name
		{
			get { return "Chinese"; }
		}

		public string NamePlural
		{
			get { return "Chinese"; }
		}

		public string LeaderName
		{
			get { return "Mao Tse Tung"; }
		}

		public byte PreferredPlayerNumber
		{
			get { return 5; }
		}

		public byte StartX
		{
			get { return 66; }
		}

		public byte StartY
		{
			get { return 19; }
		}
		
		public string[] CityNames
		{
			get
			{
				return new string[]
				{
					"Peking",
					"Shanghai",
					"Canton",
					"Nanking",
					"Tsingtao",
					"Hangchow",
					"Tientsin",
					"Tatung",
					"Macao",
					"Anyang",
					"Shantung",
					"Chinan",
					"Kaifeng",
					"Ningpo",
					"Paoting",
					"Yangchow"
				};
			}
		}
	}
}