﻿using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;
namespace QuizApp.Application.ExamAttendants;

public class CreateExamAttendantCommandHendler : ICommandHandler<CreateExamAttendantCommand, ExamAttendantResponse>
{
    private readonly IExamAttendantRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public CreateExamAttendantCommandHendler(
        IExamAttendantRepository repository,
        IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<ExamAttendantResponse>> Handle(
        CreateExamAttendantCommand request,
        CancellationToken cancellationToken)
    {
        ExamAttendant attendant =ExamAttendantMapper.MapToExamAttendant(request);

        repository.Insert(attendant);

        await unitOfWork.SaveChangesAsync();

        var response = ExamAttendantMapper.MapToExamAttendantResponse(attendant);

        return response;
    }
}
