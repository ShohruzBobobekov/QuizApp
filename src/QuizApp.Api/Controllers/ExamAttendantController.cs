﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.ExamAttendants;
using QuizApp.Application.ExamAttendants.GetExamAttendantsByExam;

namespace QuizApp.Api.Controllers;

[Route("api/examAttendant")]
public class ExamAttendantController : ApiController
{
    public ExamAttendantController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateExamAttendant(
        CreateExamAttendantCommand command,
        CancellationToken token)
    {
        var response = await Sender.Send(command, token);

        if(response.IsFailure)
        {
            return HandleFailure(response);
        }
        return Ok(response);
    }
    [HttpGet("examId:Guid")]
    public async ValueTask<IActionResult> GetExamAttendantByExamId(
        Guid examId,
        CancellationToken token)
    {
        var query = new GetExamAttendantByExamQuery(examId);
        var response = await Sender.Send(query, token);

        if(response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response);
    }
}