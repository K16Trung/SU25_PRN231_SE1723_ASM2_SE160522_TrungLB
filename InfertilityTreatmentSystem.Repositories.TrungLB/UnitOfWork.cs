using InfertilityTreatmentSystem.Repositories.TrungLB.DBContext;
using System;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Repositories.TrungLB
{
    public interface IUnitOfWork : IDisposable
    {
        TreatmentReminderTrungLbRepository TreatmentReminderRepository { get; }
        ReminderTypeTrungLbRepository ReminderTypeRepository { get; }
        SystemUserAccountRepository SystemUserAccountRepository { get; }
        int SaveChangeWithTransaction();
        Task<int> SaveChangesWithTransactionAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly Su25Prn231Se1723G2InfertilityTreatmentServiceContext _context;
        private TreatmentReminderTrungLbRepository _treatmentReminderRepository;
        private ReminderTypeTrungLbRepository _reminderTypeRepository;
        private SystemUserAccountRepository _systemUserAccountRepository;

        public UnitOfWork() => _context ??= new Su25Prn231Se1723G2InfertilityTreatmentServiceContext();

        public TreatmentReminderTrungLbRepository TreatmentReminderRepository
        {
            get { return _treatmentReminderRepository ??= new TreatmentReminderTrungLbRepository(_context); }
        }

        public ReminderTypeTrungLbRepository ReminderTypeRepository
        {
            get { return _reminderTypeRepository ??= new ReminderTypeTrungLbRepository(_context); }
        }

        public SystemUserAccountRepository SystemUserAccountRepository
        {
            get { return _systemUserAccountRepository ??= new SystemUserAccountRepository(_context); }
        }

        public void Dispose() => _context.Dispose();

        public int SaveChangeWithTransaction()
        {
            int result = -1;

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
    }
}