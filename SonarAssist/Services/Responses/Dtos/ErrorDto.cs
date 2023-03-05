using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Responses.Dtos
{
	public class ErrorDto : ISonarResponseDto
	{
		public Error[] errors { get; set; }
	}

	public class Error
	{
		public string msg { get; set; } = string.Empty;
	}

}
