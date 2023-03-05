using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Responses.Dtos
{
	public class SearchProjectDto : ISonarResponseDto
	{
		public Paging paging { get; set; }
		public Component[] components { get; set; }
	}

	public class Paging
	{
		public int pageIndex { get; set; }
		public int pageSize { get; set; }
		public int total { get; set; }
	}

	public class Component
	{
		public string key { get; set; }
		public string name { get; set; }
		public string qualifier { get; set; }
		public string visibility { get; set; }
		public DateTime lastAnalysisDate { get; set; }
		public string revision { get; set; }
	}

}
