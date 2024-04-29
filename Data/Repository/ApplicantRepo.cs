using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Simple_API_Assessment.Model;

namespace Simple_API_Assessment.Data.Repository;

public class ApplicantRepo : IApplicantRepository {
	private readonly DataContext _context;

	public ApplicantRepo(DataContext context) {
		_context = context;
	}

	public async Task<IEnumerable<Applicant>> GetAllApplicants() {
		return await Task.FromResult<IEnumerable<Applicant>>(_context.Applicants.Include("Skills").ToList());
	}

	public async Task<Applicant?> GetApplicantById(long id) {
		return await _context.Applicants.Include("Skills").FirstOrDefaultAsync(ap => ap.Id == id);
	}

	public async Task<bool> CreateApplicant(Applicant applicant) {
		var existingApplicant = await GetApplicantById(applicant.Id);

		if (existingApplicant != null) throw new Exception("Applicant Id: " + applicant.Id + " already exists");

		await _context.Applicants.AddAsync(applicant);
		if (applicant.Skills != null) await _context.Skills.AddRangeAsync(applicant.Skills);
		var res = await _context.SaveChangesAsync();
		return res > 0;
	}

	public async Task<bool> UpdateApplicant(Applicant applicant) {
		var existingApplicant = GetApplicantById(applicant.Id).Result;

		if (existingApplicant == null) throw new Exception("Applicant Id: " + applicant.Id + " does not exists");

		var applicantSkills = existingApplicant.Skills?.ToList();
		
		existingApplicant.Name = applicant.Name;

		if (applicant.Skills != null) {
			foreach (var skill in applicant.Skills.ToList()) {
				var existingSkill = applicantSkills?.FirstOrDefault(sk => sk.Id == skill.Id && sk.ApplicantId == applicant.Id);
				
				if (existingSkill != null) {
					existingSkill.Name = skill.Name;
				}
				await _context.Skills.AddAsync(new Skill() {Name = skill.Name, ApplicantId = existingApplicant.Id});
			}
		}

		var res = await _context.SaveChangesAsync();
		return res > 0;
	}


	public async Task<bool> DeleteApplicant(long id) {
		var applicant = await _context.Applicants.FindAsync(id);

		if (applicant == null) throw new ValidationException("Applicant with the Id: " + id + " does not exists");
		
		_context.Applicants.Remove(applicant);
		var res = await _context.SaveChangesAsync();
		return res > 0;

	}
}