﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Reflection;
using WizardFormBackend.Data.Context;
using WizardFormBackend.Data.Models;
using WizardFormBackend.Utils;

namespace WizardFormBackend.Data.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly WizardFormDbContext _dbContext;
        public RequestRepository(WizardFormDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Request>> GetAllRequestAsync(string searchKeyword)
        {
            searchKeyword = Util.Sanitize(searchKeyword.ToLower());

            return await _dbContext.Requests.Where(r =>
                r.RequestId.ToString() == searchKeyword ||
                r.UserId.ToString() == searchKeyword ||
                r.Title.ToLower().Contains(searchKeyword) ||
                r.PriorityCode == (int)Constants.PriorityCode.High && "high".Contains(searchKeyword) ||
                r.PriorityCode == (int)Constants.PriorityCode.Normal && "normal".Contains(searchKeyword) ||
                r.PriorityCode == (int)Constants.PriorityCode.Low && "low".Contains(searchKeyword) ||
                r.StatusCode == (int)Constants.StatusCode.Pending && "pending".Contains(searchKeyword) ||
                r.StatusCode == (int)Constants.StatusCode.Approved && "approved".Contains(searchKeyword) ||
                r.StatusCode == (int)Constants.StatusCode.Rejected && "rejected".Contains(searchKeyword)
            ).ToListAsync();
        }

        public async Task<Request?> GetRequestByRequestIdAsync(long requestId)
        {
            return await _dbContext.Requests.FindAsync(requestId);
        }

        public async Task<IEnumerable<Request>> GetAllRequestByUserIdAsync(long userId, string searchKeyword)
        {
            searchKeyword = Util.Sanitize(searchKeyword.ToLower());

            return await _dbContext.Requests.Where(r => r.UserId == userId).Where(r =>
                r.RequestId.ToString() == searchKeyword ||
                r.RequestDate.ToString().ToLower().Contains(searchKeyword) ||
                r.Title.ToLower().Contains(searchKeyword) ||
                r.StatusCode == (int)Constants.StatusCode.Pending && "pending".Contains(searchKeyword) ||
                r.StatusCode == (int)Constants.StatusCode.Approved && "approved".Contains(searchKeyword) ||
                r.StatusCode == (int)Constants.StatusCode.Rejected && "rejected".Contains(searchKeyword)
            ).ToListAsync();
        }

        public async Task<Request> AddRequestAsync(Request request)
        {
            _dbContext.Requests.Add(request);
            await _dbContext.SaveChangesAsync();
            return request;
        }

        public async Task UpdateRequestAsync(Request request)
        {
            _dbContext.Requests.Update(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRequestAsync(Request request)
        {
            _dbContext.Requests.Remove(request);
            await _dbContext.SaveChangesAsync();
        }
    }
}
