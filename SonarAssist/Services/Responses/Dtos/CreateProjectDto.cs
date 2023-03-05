using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Responses.Dtos
{
	public class CreateProjectDto : ISonarResponseDto
	{
		public Project project { get; set; }
	}

	public class Project
	{
		public string key { get; set; }
		public string name { get; set; }
		public string qualifier { get; set; }
		public string visibility { get; set; }
	}

}
