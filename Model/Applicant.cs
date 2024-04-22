using System;
using System.Collections.Generic;

namespace Simple_API_Assessment.Model;

public class Applicant {
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;
	public IEnumerable<Skill>? Skills { get; set; }
}