using GraphQL;
using GraphQL.Client.Abstractions;
using InfertilityTreatmentSystem.GraphQLClients.BlazorWAS.TrungLB.Models;

namespace TreatmentReminder.GraphQLClients.BlazorWAS.TrungLB.GraphQLClients
{
    public class GraphQLConsumer
    {
        private readonly IGraphQLClient _graphQLClient;

        public GraphQLConsumer(IGraphQLClient graphQLClient) => _graphQLClient = graphQLClient;

        #region TreatmentReminder Queries
        public async Task<List<TreatmentReminderTrungLb>> GetTreatmentReminderTrungLbs()
        {
            try
            {
                var query = @"
                query TreatmentReminderTrungLbs {
                    treatmentReminderTrungLbs {
                        reminderId
                        title
                        message
                        reminderDate
                        createdAt
                        isSent
                        isRecurring
                        patientName
                        relatedDoctor
                        reminderTypeId
                        reminderType {
                            reminderTypeId
                            name
                            description
                        }
                    }
                }";

                var response = await _graphQLClient.SendQueryAsync<TreatmentReminderTrungLbsGraphQLResponse>(query);
                
                if (response.Errors != null && response.Errors.Any())
                {
                    var errorMessages = string.Join(", ", response.Errors.Select(e => e.Message));
                    throw new Exception($"GraphQL Errors: {errorMessages}");
                }

                var result = response?.Data?.treatmentReminderTrungLbs;
                return result ?? new List<TreatmentReminderTrungLb>();
            }
            catch (Exception ex)
            {
                throw; // Re-throw to let the UI handle it
            }
        }

        // New method to get total count for pagination
        public async Task<int> GetTreatmentReminderTotalCount()
        {
            try
            {
                var allReminders = await GetTreatmentReminderTrungLbs();
                return allReminders.Count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<TreatmentReminderTrungLb?> GetTreatmentReminderById(int id)
        {
            try
            {
                var graphRequest = new GraphQLRequest
                {
                    Query = @"
                    query TreatmentReminderByIds($id: Int!) {
                        treatmentReminderByIds(id: $id) {
                            reminderId
                            title
                            message
                            reminderDate
                            createdAt
                            isSent
                            isRecurring
                            patientName
                            relatedDoctor
                            reminderTypeId
                            reminderType {
                                reminderTypeId
                                name
                                description
                            }
                        }
                    }",
                    Variables = new { id = id }
                };

                var response = await _graphQLClient.SendQueryAsync<TreatmentReminderTrungLbGraphQLResponse>(graphRequest);
                
                if (response.Errors != null && response.Errors.Any())
                {
                    var errorMessages = string.Join(", ", response.Errors.Select(e => e.Message));
                    throw new Exception($"GraphQL Errors: {errorMessages}");
                }

                var result = response?.Data?.treatmentReminderByIds;
                return result;
            }
            catch (Exception ex)
            {
                throw; // Re-throw to let the UI handle it
            }
        }

        public async Task<List<TreatmentReminderTrungLb>> SearchTreatmentRemindersWithPaging(SearchTreatmentReminderRequest request)
        {
            try
            {
                var graphRequest = new GraphQLRequest
                {
                    Query = @"
                    query SearchTreatmentReminderTrungLbWithPagings($request: SearchTreatmentReminderRequestInput!) {
                        searchTreatmentReminderTrungLbWithPagings(request: $request) {
                            reminderId
                            title
                            message
                            reminderDate
                            createdAt
                            isSent
                            isRecurring
                            patientName
                            relatedDoctor
                            reminderTypeId
                            reminderType {
                                reminderTypeId
                                name
                                description
                            }
                        }
                    }",
                    Variables = new { request = request }
                };

                var response = await _graphQLClient.SendQueryAsync<SearchTreatmentReminderTrungLbWithPagingsGraphQLResponse>(graphRequest);
                
                if (response.Errors != null && response.Errors.Any())
                {
                    var errorMessages = string.Join(", ", response.Errors.Select(e => e.Message));
                    throw new Exception($"GraphQL Errors: {errorMessages}");
                }

                var result = response?.Data?.searchTreatmentReminderTrungLbWithPagings;
                return result ?? new List<TreatmentReminderTrungLb>();
            }
            catch (Exception ex)
            {
                throw; // Re-throw to let the UI handle it
            }
        }
        #endregion

        #region TreatmentReminder Mutations
        public async Task<int> CreateTreatmentReminder(TreatmentReminderTrungLb reminder)
        {
            try
            {
                // Convert to input DTO
                var input = new TreatmentReminderInput
                {
                    ReminderId = reminder.ReminderId,
                    Title = reminder.Title,
                    Message = reminder.Message,
                    ReminderDate = reminder.ReminderDate,
                    CreatedAt = reminder.CreatedAt,
                    IsSent = reminder.IsSent,
                    IsRecurring = reminder.IsRecurring,
                    PatientName = reminder.PatientName,
                    RelatedDoctor = reminder.RelatedDoctor,
                    ReminderTypeId = reminder.ReminderTypeId
                };

                var mutation = new GraphQLRequest
                {
                    Query = @"
                    mutation CreateTreatmentReminder($reminder: TreatmentReminderTrungLbInput!) {
                        createTreatmentReminderTrungLbs(request: $reminder)
                    }",
                    Variables = new { reminder = input }
                };

                var response = await _graphQLClient.SendMutationAsync<CreateTreatmentReminderGraphQLResponse>(mutation);
                return response?.Data?.createTreatmentReminderTrungLbs ?? 0;
            }
            catch (Exception ex)
            {
                // Log exception if needed
            }
            return 0;
        }

        public async Task<int> UpdateTreatmentReminder(TreatmentReminderTrungLb reminder)
        {
            try
            {
                // Convert to input DTO
                var input = new TreatmentReminderInput
                {
                    ReminderId = reminder.ReminderId,
                    Title = reminder.Title,
                    Message = reminder.Message,
                    ReminderDate = reminder.ReminderDate,
                    CreatedAt = reminder.CreatedAt,
                    IsSent = reminder.IsSent,
                    IsRecurring = reminder.IsRecurring,
                    PatientName = reminder.PatientName,
                    RelatedDoctor = reminder.RelatedDoctor,
                    ReminderTypeId = reminder.ReminderTypeId
                };

                var mutation = new GraphQLRequest
                {
                    Query = @"
                    mutation UpdateTreatmentReminder($reminder: TreatmentReminderTrungLbInput!) {
                        updateTreatmentReminderTrungLbs(request: $reminder)
                    }",
                    Variables = new { reminder = input }
                };

                var response = await _graphQLClient.SendMutationAsync<UpdateTreatmentReminderGraphQLResponse>(mutation);
                return response?.Data?.updateTreatmentReminderTrungLbs ?? 0;
            }
            catch (Exception ex)
            {
                // Log exception if needed
            }
            return 0;
        }

        public async Task<bool> DeleteTreatmentReminder(int id)
        {
            try
            {
                Console.WriteLine($"Attempting to delete treatment reminder with ID: {id}");
                
                var mutation = new GraphQLRequest
                {
                    Query = @"
                    mutation DeleteTreatmentReminder($id: Int!) {
                        deleteTreatmentReminderTrungLbs(id: $id)
                    }",
                    Variables = new { id = id }
                };

                var response = await _graphQLClient.SendMutationAsync<DeleteTreatmentReminderGraphQLResponse>(mutation);
                
                if (response.Errors != null && response.Errors.Any())
                {
                    var errorMessages = string.Join(", ", response.Errors.Select(e => e.Message));
                    Console.WriteLine($"GraphQL delete errors: {errorMessages}");
                    throw new Exception($"GraphQL Errors: {errorMessages}");
                }
                
                var result = response?.Data?.deleteTreatmentReminderTrungLbs ?? false;
                Console.WriteLine($"Delete result: {result}");
                
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in DeleteTreatmentReminder: {ex.Message}");
                throw; // Re-throw to let the UI handle it
            }
        }

        // Add missing method for marking reminders as sent
        public async Task<TreatmentReminderTrungLb?> MarkTreatmentReminderAsSent(int id)
        {
            try
            {
                // First get the current reminder
                var reminder = await GetTreatmentReminderById(id);
                if (reminder == null) return null;

                // Mark it as sent
                reminder.IsSent = true;
                
                // Update it
                var result = await UpdateTreatmentReminder(reminder);
                
                return result > 0 ? reminder : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MarkTreatmentReminderAsSent: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region ReminderType Queries
        public async Task<List<ReminderTypeTrungLb>> GetReminderTypeTrungLbs()
        {
            try
            {
                var query = @"
                query ReminderTypeTrungLbs {
                    reminderTypeTrungLbs {
                        reminderTypeId
                        name
                        description
                    }
                }";

                var response = await _graphQLClient.SendQueryAsync<ReminderTypeTrungLbsGraphQLResponse>(query);
                var result = response?.Data?.reminderTypeTrungLbs;

                return result ?? new List<ReminderTypeTrungLb>();
            }
            catch (Exception ex)
            {
                // Silently handle errors - return empty list
            }

            return new List<ReminderTypeTrungLb>();
        }

        public async Task<ReminderTypeTrungLb> GetReminderTypeById(int id)
        {
            try
            {
                var graphRequest = new GraphQLRequest
                {
                    Query = @"
                    query ReminderTypeByIds($id: Int!) {
                        reminderTypeByIds(id: $id) {
                            reminderTypeId
                            name
                            description
                        }
                    }",
                    Variables = new { id = id }
                };

                var response = await _graphQLClient.SendQueryAsync<ReminderTypeTrungLbGraphQLResponse>(graphRequest);
                var result = response?.Data?.reminderTypeByIds;
                return result ?? new ReminderTypeTrungLb();
            }
            catch (Exception ex)
            {
                // Log exception if needed
            }
            return new ReminderTypeTrungLb();
        }
        #endregion

        #region ReminderType Mutations
        public async Task<int> CreateReminderType(ReminderTypeTrungLb reminderType)
        {
            try
            {
                // Convert to input DTO
                var input = new ReminderTypeInput
                {
                    ReminderTypeId = reminderType.ReminderTypeId,
                    Name = reminderType.Name,
                    Description = reminderType.Description
                };

                var mutation = new GraphQLRequest
                {
                    Query = @"
                    mutation CreateReminderType($reminderType: ReminderTypeTrungLbInput!) {
                        createReminderTypeTrungLbs(request: $reminderType)
                    }",
                    Variables = new { reminderType = input }
                };

                var response = await _graphQLClient.SendMutationAsync<CreateReminderTypeGraphQLResponse>(mutation);
                return response?.Data?.createReminderTypeTrungLbs ?? 0;
            }
            catch (Exception ex)
            {
                // Log exception if needed
            }
            return 0;
        }

        public async Task<int> UpdateReminderType(ReminderTypeTrungLb reminderType)
        {
            try
            {
                // Convert to input DTO
                var input = new ReminderTypeInput
                {
                    ReminderTypeId = reminderType.ReminderTypeId,
                    Name = reminderType.Name,
                    Description = reminderType.Description
                };

                var mutation = new GraphQLRequest
                {
                    Query = @"
                    mutation UpdateReminderType($reminderType: ReminderTypeTrungLbInput!) {
                        updateReminderTypeTrungLbs(request: $reminderType)
                    }",
                    Variables = new { reminderType = input }
                };

                var response = await _graphQLClient.SendMutationAsync<UpdateReminderTypeGraphQLResponse>(mutation);
                return response?.Data?.updateReminderTypeTrungLbs ?? 0;
            }
            catch (Exception ex)
            {
                // Log exception if needed
            }
            return 0;
        }

        public async Task<bool> DeleteReminderType(int id)
        {
            try
            {
                var mutation = new GraphQLRequest
                {
                    Query = @"
                    mutation DeleteReminderType($id: Int!) {
                        deleteReminderTypeTrungLbs(id: $id)
                    }",
                    Variables = new { id = id }
                };

                var response = await _graphQLClient.SendMutationAsync<DeleteReminderTypeGraphQLResponse>(mutation);
                return response?.Data?.deleteReminderTypeTrungLbs ?? false;
            }
            catch (Exception ex)
            {
                // Log exception if needed
            }
            return false;
        }
        #endregion

        #region Authentication Queries
        public async Task<SystemUserAccount?> AuthenticateUser(string username, string password)
        {
            try
            {
                var graphRequest = new GraphQLRequest
                {
                    Query = @"
                    query AuthenticateUser($username: String!, $password: String!) {
                        authenticateUser(username: $username, password: $password) {
                            userAccountId
                            userName
                            fullName
                            email
                            phone
                            employeeCode
                            roleId
                            isActive
                        }
                    }",
                    Variables = new { username = username, password = password }
                };

                var response = await _graphQLClient.SendQueryAsync<AuthenticationGraphQLResponse>(graphRequest);
                
                if (response.Errors != null && response.Errors.Any())
                {
                    var errorMessages = string.Join(", ", response.Errors.Select(e => e.Message));
                    throw new Exception($"GraphQL Errors: {errorMessages}");
                }

                return response?.Data?.authenticateUser;
            }
            catch (Exception ex)
            {
                throw; // Re-throw to let the UI handle it
            }
        }
        #endregion
    }
}