using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sello.Application.DTO;
using Sello.Application.ServiceContracts;
using Sello.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<PagedResultResponseDto<UserResponseDto>> GetAllUsers(UserQueryParamsDto queryParams)
        {
            ParamsChecking(ref queryParams);

            var usersQuery = _userRepository.GetAllUsers();
            if (!string.IsNullOrWhiteSpace(queryParams.Search))
            {
                usersQuery = usersQuery.Where(u =>
                    u.Email!.Contains(queryParams.Search) ||
                    u.PersonName!.Contains(queryParams.Search));
            }

            var totalCount = await usersQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize);

            var users = await usersQuery
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ProjectTo<UserResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return new PagedResultResponseDto<UserResponseDto>
            {
                Items = users,
                TotalCount = totalCount,
                PageNumber = queryParams.PageNumber,
                PageSize = queryParams.PageSize,
                TotalPages = totalPages
            };
        }


        private void ParamsChecking(ref UserQueryParamsDto queryParams)
        {
            const int maxPageSize = 50;

            queryParams.PageNumber = queryParams.PageNumber < 1
                ? 1
                : queryParams.PageNumber;

            queryParams.PageSize = queryParams.PageSize < 1
                ? 10
                : queryParams.PageSize;

            if (queryParams.PageSize > maxPageSize)
                queryParams.PageSize = maxPageSize;
        }
    }
}
