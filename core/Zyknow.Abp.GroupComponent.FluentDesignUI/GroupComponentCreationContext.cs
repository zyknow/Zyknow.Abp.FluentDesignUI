﻿using Volo.Abp.DependencyInjection;

namespace Zyknow.Abp.GroupComponent.FluentDesignUI;

public class GroupComponentCreationContext(IServiceProvider serviceProvider) : IServiceProviderAccessor
{
    public IServiceProvider ServiceProvider { get; } = serviceProvider;

    public List<ComponentGroup> Groups { get; } = new();
}

