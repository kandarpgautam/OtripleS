﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using FluentAssertions;
using OtripleS.Web.Api.Models.Guardians;
using OtripleS.Web.Api.Models.StudentGuardians;
using OtripleS.Web.Api.Models.Students;
using OtripleS.Web.Api.Tests.Acceptance.Brokers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace OtripleS.Web.Api.Tests.Acceptance.APIs.StudentGuardians
{
    public partial class StudentGuardiansApiTests    
    {
        [Fact]
        public async Task ShouldPostStudentGuardianAsync()
        {
            // given
            Student persistedStudent = await PostStudentAsync();
            Guardian persistedGuardian = await PostGuardianAsync();

            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian();
            randomStudentGuardian.StudentId = persistedStudent.Id;
            randomStudentGuardian.GuardianId = persistedGuardian.Id;
            StudentGuardian inputStudentGuardian = randomStudentGuardian;
            StudentGuardian expectedStudentGuardian = inputStudentGuardian;

            // when             
            StudentGuardian persistedStudentGuardian =
                await this.otripleSApiBroker.PostStudentGuardianAsync(inputStudentGuardian);

            StudentGuardian actualStudentGuardian =
                await this.otripleSApiBroker.GetStudentGuardianAsync(persistedStudentGuardian.StudentId, persistedStudentGuardian.GuardianId);

            // then
            actualStudentGuardian.Should().BeEquivalentTo(expectedStudentGuardian);

            await this.otripleSApiBroker.DeleteStudentGuardianAsync(actualStudentGuardian.StudentId,
                actualStudentGuardian.GuardianId);
            await this.otripleSApiBroker.DeleteGuardianByIdAsync(actualStudentGuardian.GuardianId);
            await this.otripleSApiBroker.DeleteStudentByIdAsync(actualStudentGuardian.StudentId);
        }

        private async Task<Student> PostStudentAsync()
        {
            Student randomStudent = CreateRandomStudent();
            Student inputStudent = randomStudent;
            
            return await this.otripleSApiBroker.PostStudentAsync(inputStudent);            
        }

        private async Task<Guardian> PostGuardianAsync()
        {
            Guardian randomGuardian = CreateRandomGuardian();
            Guardian inputGuardian = randomGuardian;

           return await this.otripleSApiBroker.PostGuardianAsync(inputGuardian);
        }
    }
}
