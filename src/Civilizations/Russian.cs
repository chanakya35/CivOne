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
	internal class Russian : ICivilization
	{
		public int Id
		{
			get
			{
				return 8;
			}
		}

		public string Name
		{
			get { return "Russian"; }
		}

		public string NamePlural
		{
			get { return "Russians"; }
		}

		public string LeaderName
		{
			get { return "Stalin"; }
		}

		public byte PreferredPlayerNumber
		{
			get { return 1; }
		}

		public byte StartX
		{
			get { return 44; }
		}

		public byte StartY
		{
			get { return 12; }
		}
		
		public string[] CityNames
		{
			get
			{
				return new string[]
				{
					"Moscow",
					"Leningrad",
					"Kiev",
					"Minsk",
					"Smolensk",
					"Odessa",
					"Sevastopol",
					"Tblisi",
					"Sverdlovsk",
					"Yakutsk",
					"Vladivostok",
					"Novograd",
					"Krasnoyarsk",
					"Riga",
					"Rostov",
					"Atrakhan"
				};
			}
		}
	}
}