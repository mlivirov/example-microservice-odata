﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectName.Sso.Application.Database.Configuration
{
    public interface IEntityTypeBuilder<TEntity> where TEntity: class
    {
        void Build(EntityTypeBuilder<TEntity> modelBuilder);
    }
}