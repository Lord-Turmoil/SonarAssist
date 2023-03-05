using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services
{
	public enum ServiceStatus
	{
		OK,
		Unauthorized,
		Forbidden,
		NotFound,
		BadRequest,
		Error,

		// For logic status.
		YES,
		NO
	}
}
