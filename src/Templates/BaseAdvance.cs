// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CivOne.Enums;
using CivOne.GFX;
using CivOne.Interfaces;

namespace CivOne.Templates
{
	internal abstract class BaseAdvance : BaseInstance, IAdvance
	{
		private Advance[] _requiredTechs;
		private IEnumerable<IAdvance> GetRequiredTechs()
		{
			foreach (Advance advance in _requiredTechs)
			{
				yield return Common.Advances.Where(x => x.Id == (byte)advance).FirstOrDefault();
			}
		}
		
		public virtual Picture Icon { get; protected set; }
		
		public string Name { get; protected set; }
		public byte PageCount
		{
			get
			{
				return 2;
			}
		}
		public Picture DrawPage(byte pageNumber)
		{
			Picture output = new Picture(320, 200);
			
			int yy;
			switch (pageNumber)
			{
				case 1:
					string[] text = new string[0];
					text = Resources.Instance.GetCivilopediaText("BLURB0/" + Name.ToUpper());
					
					yy = 76;
					foreach (string line in text)
					{
						Console.WriteLine(line);
						output.DrawText(line, 6, 1, 12, yy);
						yy += 9;
					}
					
					break;
				case 2:
					yy = 84;
					if (pageNumber == 2)
					{
						if (RequiredTechs.Length > 0)
						{
							StringBuilder requiredTech = new StringBuilder();
							foreach (IAdvance tech in RequiredTechs)
							{
								if (requiredTech.Length > 0)
									requiredTech.Append(" and ");
								requiredTech.Append(tech.Name);
							}
							output.DrawText(string.Format("Requires {0}", requiredTech), 6, 1, 32, yy); yy += 8;
						}
						yy += 16;
						output.DrawText("Allows:", 6, 1, 32, yy); yy += 8;
						foreach (IAdvance tech in Common.Advances.Where(a => a.Requires(Id)))
						{
							string allows = tech.Name;
							foreach (IAdvance at in tech.RequiredTechs.Where(a => a.Id != Id))
								allows += string.Format(" (with {0})", at.Name);
							output.DrawText(allows, 6, 9, 40, yy); yy += 8;
						}
						yy += 4;
						foreach (IUnit unit in Reflect.GetUnits().Where(u => u.RequiredTech != null && u.RequiredTech.Id == Id))
						{
							output.AddLayer(unit.GetUnit(Game.PlayerNumber(Human)).Image, 40, yy - 5);
							output.DrawText(string.Format("{0} unit", unit.Name), 6, 12, 60, yy); yy += 12;
						}
						foreach (IBuilding building in Reflect.GetBuildings().Where(b => b.RequiredTech != null && b.RequiredTech.Id == Id))
						{
							if (building.SmallIcon != null)
								output.AddLayer(building.SmallIcon.Image, 39, yy - 2);
							output.DrawText(string.Format("{0} improvement", building.Name), 6, 2, 60, yy); yy += 12;
						}
						foreach (IWonder wonder in Reflect.GetWonders().Where(w => w.RequiredTech != null && w.RequiredTech.Id == Id))
						{
							if (wonder.SmallIcon != null)
								output.AddLayer(wonder.SmallIcon.Image, 39, yy - 2);
							output.DrawText(string.Format("{0} Wonder", wonder.Name), 6, 2, 60, yy); yy += 12;
						}
					}
					break;
				default:
					Console.WriteLine("Invalid page number: {0}", pageNumber);
					break;
			}
			
			return output;
		}
		
		protected Advance Type { get; set; }
		
		public IAdvance[] RequiredTechs
		{
			get
			{
				return GetRequiredTechs().ToArray();
			}
		}
		
		protected void SetIcon(int page, int col, int row)
		{
			int xx = 1 + (111 * col);
			int yy = 1 + (69 * row);
			int ww = col < 2 ? 112 : 96;
			int hh = row < 2 ? 68 : 60;
			
			Bitmap icon = Resources.Instance.GetPart(string.Format("ICONPG{0}", page), xx, yy, ww, hh);
			
			Icon = new Picture(112, 68, icon.Palette.Entries);
			Icon.AddLayer(icon, col < 2 ? 0 : 7, row < 2 ? 0 : 4);
			Icon.FillRectangle(0, 110, 0, 2, 68);
		}
		
		public byte Id
		{
			get
			{
				return (byte)Type;
			}
		}
		
		public bool Requires(byte id)
		{
			foreach (IAdvance tech in GetRequiredTechs())
				if (tech.Id == id) return true;
			return false;
		}
		
		protected BaseAdvance(params Advance[] requiredTechs)
		{
			_requiredTechs = requiredTechs;
		}
	}
}