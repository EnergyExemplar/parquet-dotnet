﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Parquet.Data;
using Parquet.File;
using Xunit;

namespace Parquet.Test
{
   public class ParquetWriter3Test
   {
      //[Fact]
      public void SmokeWrite()
      {
         var schema = new Schema(new DataField<int>("id"), new DataField<string>("name"));

         using (var ms = new MemoryStream())
         {
            using (var writer = new ParquetWriter3(schema, ms))
            {
               using (ParquetRowGroupWriter group = writer.CreateRowGroup(3))
               {
                  group.Write(new[] { 1, 2, 3 });
                  group.Write(new[] { "first", "second", "third" });
               }
            }
         }
      }
   }
}
