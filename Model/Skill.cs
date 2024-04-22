using System;

namespace Simple_API_Assessment.Model;

public class Skill {
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;
	public Guid ApplicantId { get; set; }
}