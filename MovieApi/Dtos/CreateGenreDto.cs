﻿namespace MovieApi.Dtos
{
    public class CreateGenreDto
    {
        [MaxLength(100)]
        public String Name { get; set; }
    }
}
