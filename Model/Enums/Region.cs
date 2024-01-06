using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enums
{
	public enum Region
	{
		Unknown,
		Other,
		[Description("United Kingdom")]
		TheUK,
		[Description("United States of America")]
		TheUSA,
		Canada,
		Germany,
		France,
		Spain,
		China,
		Japan,
		India,
		Australia,
		Brazil,
		Turkey
	}
}
