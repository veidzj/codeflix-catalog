﻿using System;

namespace Codeflix.Catalog.Domain.Exceptions;
public class EntityValidationException(string? message) : Exception(message)
{
}
