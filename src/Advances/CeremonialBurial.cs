// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using CivOne.Enums;
using CivOne.Templates;

namespace CivOne.Advances
{
	internal class CeremonialBurial : BaseAdvance
	{
		public CeremonialBurial() : base()
		{
			Name = "Ceremonial Burial";
			Type = Advance.CeremonialBurial;
			SetIcon(8, 2, 0);
		}
	}
}