using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Simple_API_Assessment.Data.Repository;
using Simple_API_Assessment.Model;

namespace Simple_API_Assessment.Controllers;

[ApiController]
[Route("api/v1/applicant")]
[Produces(MediaTypeNames.Application.Json)]
public class ApplicantController : ControllerBase {
	private readonly IApplicantRepository _applicantRepository;

	public ApplicantController(IApplicantRepository applicantRepository) {
		_applicantRepository = applicantRepository;
	}

	[HttpGet("all")]
	public ActionResult<IEnumerable<Applicant>> GetAllApplicant() {
		var resp = _applicantRepository.GetAllApplicants().Result;
		if (resp.Any()) {
			return Ok(resp);
		}

		return NotFound("There are no Applicants");
	}

	[HttpGet("{id}")]
	public ActionResult<Applicant> GetApplicantById(long id) {
		var resp = _applicantRepository.GetApplicantById(id).Result;
		if (resp != null) {
			return Ok(resp);
		}

		return NotFound("Applicant with Id: " + id + " does not exists!");
	}

	[HttpPost("add")]
	[Consumes(MediaTypeNames.Application.Json)]
	public ActionResult AddApplicant([FromBody] Applicant applicant) {
		var resp = _applicantRepository.CreateApplicant(applicant);
		if (resp.Result) {
			return CreatedAtAction(nameof(AddApplicant), new { id = applicant.Id }, applicant);
		}

		return BadRequest("Unable to create Applicant");
	}

	[HttpPut("update/{id}")]
	[Consumes(MediaTypeNames.Application.Json)]
	public ActionResult UpdateApplicant([FromBody] Applicant applicant, long id) {
		try {
			applicant.Id = id;
			var resp = _applicantRepository.UpdateApplicant(applicant);
			if (resp.Result) {
				return CreatedAtAction(nameof(UpdateApplicant), new { name = applicant.Name }, applicant);
			}

			return BadRequest("Error updating Applicant with Id : " + id);
		}
		catch (Exception e) {
			Console.WriteLine(e);
			return BadRequest("Unable to update Applicant");
		}
	}

	[HttpDelete("{id}")]
	public ActionResult DeleteApplicant(long id) {
		var resp = _applicantRepository.DeleteApplicant(id);
		if (resp.Result) {
			return Ok("Applicant deleted successfully");
		}

		return NoContent();
	}
}