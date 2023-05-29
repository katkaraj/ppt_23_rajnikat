﻿using System;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;

namespace Ppt23.Api.Data
{
	public class PptDbContext : DbContext
	{
		public PptDbContext(DbContextOptions<PptDbContext> options) :base(options)
		{
		}

        public DbSet<Vybaveni> Vybavenis => Set<Vybaveni>();
		public DbSet<Revize> Revizes => Set<Revize>();
		public DbSet<Ukon> Ukons => Set<Ukon>();
    }
}

