using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.CareerAdvancement.Core.Data
{
    public sealed class CareerCommandDbContext(
    DbContextOptions<CareerCommandDbContext> options)
    : CareerAdvancementSchemeDbContext(options)
    {
    }
}
