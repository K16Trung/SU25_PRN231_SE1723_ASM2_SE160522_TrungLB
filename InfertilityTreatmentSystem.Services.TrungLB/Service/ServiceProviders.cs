using InfertilityTreatmentSystem.Services.TrungLB.Service.IService;
using System;

namespace InfertilityTreatmentSystem.Services.TrungLB.Service
{
    public interface IServiceProviders
    {
        IReminderTypeTrungLbService ReminderTypeTrungLbService { get; }
        ITreatmentReminderTrungLbService TreatmentReminderTrungLbService { get; }
        SystemUserAccountService SystemUserAccountService { get; }
    }

    public class ServiceProviders : IServiceProviders
    {
        private IReminderTypeTrungLbService _reminderTypeTrungLbService;
        private ITreatmentReminderTrungLbService _treatmentReminderTrungLbService;
        private SystemUserAccountService _systemUserAccountService;

        public ServiceProviders()
        {
        }

        public IReminderTypeTrungLbService ReminderTypeTrungLbService
        {
            get { return _reminderTypeTrungLbService ??= new ReminderTypeTrungLbService(); }
        }

        public ITreatmentReminderTrungLbService TreatmentReminderTrungLbService
        {
            get { return _treatmentReminderTrungLbService ??= new TreatmentReminderTrungLbService(); }
        }

        public SystemUserAccountService SystemUserAccountService
        {
            get { return _systemUserAccountService ??= new SystemUserAccountService(); }
        }
    }
}