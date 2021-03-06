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
	internal class Zulu : ICivilization
	{
		public int Id
		{
			get
			{
				return 9;
			}
		}

		public string Name
		{
			get { return "Zulu"; }
		}

		public string NamePlural
		{
			get { return "Zulus"; }
		}

		public string LeaderName
		{
			get { return "Shaka"; }
		}

		public byte PreferredPlayerNumber
		{
			get { return 2; }
		}

		public byte StartX
		{
			get { return 42; }
		}

		public byte StartY
		{
			get { return 42; }
		}
		
		public string[] CityNames
		{
			get
			{
				return new string[]
				{
					"Zimbabwe",
					"Ulundi",
					"Bapedi",
					"Hlobane",
					"Isandhlwala",
					"Intombe",
					"Mpondo",
					"Ngome",
					"Swazi",
					"Tugela",
					"Umtata",
					"Umfolozi",
					"Ibabanago",
					"Isipezi",
					"Amatikulu",
					"Zunquin"
				};
			}
		}
	}
}