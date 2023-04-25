using System;
using Microsoft.EntityFrameworkCore;

namespace Ppt23.Api.Data
{
	public class PptDbContext : DbContext
	{
		public PptDbContext(DbContextOptions<PptDbContext> options)
		{

		}
	}
}

