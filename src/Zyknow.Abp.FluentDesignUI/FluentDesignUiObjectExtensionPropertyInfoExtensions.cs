using System.ComponentModel.DataAnnotations;
using Microsoft.FluentUI.AspNetCore.Components;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Reflection;
using Zyknow.Abp.FluentDesignUI.Components.ObjectExtending;

namespace Zyknow.Abp.FluentDesignUI;

public static class FluentDesignUiObjectExtensionPropertyInfoExtensions
{
    private static readonly HashSet<Type> NumberTypes = new HashSet<Type>
    {
        typeof(int),
        typeof(long),
        typeof(byte),
        typeof(sbyte),
        typeof(short),
        typeof(ushort),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(float),
        typeof(double),
        typeof(decimal),
        typeof(int?),
        typeof(long?),
        typeof(byte?),
        typeof(sbyte?),
        typeof(short?),
        typeof(ushort?),
        typeof(uint?),
        typeof(long?),
        typeof(ulong?),
        typeof(float?),
        typeof(double?),
        typeof(decimal?)
    };

    private static readonly HashSet<Type> TextEditSupportedAttributeTypes = new HashSet<Type>
    {
        typeof(EmailAddressAttribute),
        typeof(UrlAttribute),
        typeof(PhoneAttribute)
    };

    public static string GetDateEditInputFormatOrNull(this IBasicObjectExtensionPropertyInfo property)
    {
        if (property.IsDate())
        {
            return "{0:yyyy-MM-dd}";
        }

        if (property.IsDateTime())
        {
            return "{0:yyyy-MM-dd HH:mm}";
        }

        return null;
    }

    public static string GetTextInputValueOrNull(this IBasicObjectExtensionPropertyInfo property, object value)
    {
        if (value == null)
        {
            return null;
        }

        if (TypeHelper.IsFloatingType(property.Type))
        {
            return value.ToString()?.Replace(',', '.');
        }

        return value.ToString();
    }

    public static T GetInputValueOrDefault<T>(this IBasicObjectExtensionPropertyInfo property, object value)
    {
        if (value == null)
        {
            return default;
        }

        return (T)value;
    }

    public static TextFieldType? GetTextInputMode(this ObjectExtensionPropertyInfo propertyInfo)
    {
        foreach (var attribute in propertyInfo.Attributes)
        {
            var textRoleByAttribute = GetTextInputModeFromAttributeOrNull(attribute);
            if (textRoleByAttribute != null)
            {
                return textRoleByAttribute;
            }
        }

        return GetTextInputModeFromTypeOrNull(propertyInfo.Type)
               ?? TextFieldType.Text; //default
    }

    private static TextFieldType? GetTextInputModeFromTypeOrNull(Type type)
    {
        // if (TypeHelper.IsFloatingType(type))
        // {
        //     return "number";
        // }

        if (NumberTypes.Contains(type))
        {
            return TextFieldType.Number;
        }

        return null;
    }

    private static TextFieldType? GetTextInputModeFromAttributeOrNull(Attribute attribute)
    {
        if (attribute is EmailAddressAttribute)
        {
            return TextFieldType.Email;
        }

        if (attribute is UrlAttribute)
        {
            return TextFieldType.Url;
        }


        if (attribute is PhoneAttribute)
        {
            return TextFieldType.Tel;
        }

        if (attribute is DataTypeAttribute dataTypeAttribute)
        {
            switch (dataTypeAttribute.DataType)
            {
                case DataType.EmailAddress:
                    return TextFieldType.Email;
                case DataType.Url:
                    return TextFieldType.Url;
                case DataType.PhoneNumber:
                    return TextFieldType.Tel;
            }
        }

        return null;
    }

    public static string GetTextRole(this ObjectExtensionPropertyInfo propertyInfo)
    {
        foreach (var attribute in propertyInfo.Attributes)
        {
            var textRoleByAttribute = GetTextRoleFromAttributeOrNull(attribute);
            if (textRoleByAttribute != null)
            {
                return textRoleByAttribute;
            }
        }

        return "text"; //default
    }

    private static string GetTextRoleFromAttributeOrNull(Attribute attribute)
    {
        if (attribute is EmailAddressAttribute)
        {
            return "email";
        }

        if (attribute is UrlAttribute)
        {
            return "url";
        }

        if (attribute is DataTypeAttribute dataTypeAttribute)
        {
            switch (dataTypeAttribute.DataType)
            {
                case DataType.Password:
                    return "password";
                case DataType.EmailAddress:
                    return "email";
                case DataType.Url:
                    return "url";
            }
        }

        return null;
    }

    public static Type GetInputType(this ObjectExtensionPropertyInfo propertyInfo)
    {
        foreach (var attribute in propertyInfo.Attributes)
        {
            var inputTypeByAttribute = GetInputTypeFromAttributeOrNull(attribute);
            if (inputTypeByAttribute != null)
            {
                return inputTypeByAttribute;
            }
        }

        return GetInputTypeFromTypeOrNull(propertyInfo.Type)
               ?? typeof(TextExtensionProperty<,>); //default
    }

    private static Type GetInputTypeFromAttributeOrNull(Attribute attribute)
    {
        var hasTextEditSupport = TextEditSupportedAttributeTypes.Any(t => t == attribute.GetType());

        if (hasTextEditSupport)
        {
            return typeof(TextExtensionProperty<,>);
        }


        if (attribute is DataTypeAttribute dataTypeAttribute)
        {
            switch (dataTypeAttribute.DataType)
            {
                case DataType.Password:
                    return typeof(TextExtensionProperty<,>);
                case DataType.Date:
                    return typeof(DateTimeExtensionProperty<,>);
                case DataType.Time:
                    return typeof(TimeExtensionProperty<,>);
                case DataType.EmailAddress:
                    return typeof(TextExtensionProperty<,>);
                case DataType.Url:
                    return typeof(TextExtensionProperty<,>);
                case DataType.PhoneNumber:
                    return typeof(TextExtensionProperty<,>);
                case DataType.DateTime:
                    return typeof(DateTimeExtensionProperty<,>);
            }
        }

        return null;
    }

    private static Type GetInputTypeFromTypeOrNull(Type type)
    {
        if (type == typeof(bool))
        {
            return typeof(CheckExtensionProperty<,>);
        }

        if (type == typeof(DateTime))
        {
            return typeof(DateTimeExtensionProperty<,>);
        }

        if (NumberTypes.Contains(type))
        {
            return typeof(TextExtensionProperty<,>);
        }

        return null;
    }
}