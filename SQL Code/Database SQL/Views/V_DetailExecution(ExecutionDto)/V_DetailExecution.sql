use db_ab6e43_lkacsoftdb
go

--drop view V_DetailExecutions

Create or Alter View V_DetailExecutions
AS
    Select
        -- Columns from LKACSoft_Execution (Main Table)
        E.ExecutionID,
        E.ExecutionName,
        E.CreatedBy AS CreatedByForExecution,
        E.DateCreated,
        E.IsPeriodic,

        -- Columns from LKACSoft_ExecutionAttributesDefinition (FieldName)
        ExecutionAttributesDefinition.FieldName,

        -- Columns from LKACSoft_ExecutionAttributesValue (FieldValue)
        ExecutionAttributesValue.FieldValue,

        -- Columns from LkacSoft_User (Created By)
        createdBy.ID AS CreatedByID,
        createdBy.Username AS CreatedByUsername,
        createdBy.Firstname AS CreatedByFirstname,
        createdBy.Lastname AS CreatedByLastname,
        createdBy.Avatar AS CreatedByAvatar,
        createdBy.Address AS CreatedByAddress,
        createdBy.District AS CreatedByDistrict,
        createdBy.Dob AS CreatedByDob,
        createdBy.IsQuitJob AS CreatedByIsQuitJob,
        createdBy.DateCreate AS CreatedByDateCreate,
        createdBy.Team AS CreatedByTeam,

        -- Columns from LKACSoft_ProcessSchemaStatus
        processSchemaStatus.ProcessSchemaStatusID,
        processSchemaStatus.OrderIndex,
        processSchemaStatus.CreatedAt AS processSchemaStatusCreatedAt,

        -- Columns from LKACSoft_ProcessStatus
        processStatus.ProcessStatusID,
        processStatus.StatusName,
        processStatus.DesignatedColor,

        -- Columns from LKACSoft_ProcessSchema
        processSchema.ProcessSchemaID,
        processSchema.Name As ProcessSchemaName,
        processSchema.Description As ProcessSchemaDescription,
        processSchema.CreatedAt,
        processSchema.UpdatedAt,

        -- Columns from LKACSoft_Customer (RelatedToCustomer)
        relatedToCustomer.*,

        --Count task
        CAST(COUNT(T.TaskID) AS INT) AS TaskCount

    FROM LKACSoft_Execution E
        LEFT JOIN LKACSoft_Task T
            on T.RelatedToExecution = E.ExecutionID
        Left join LkacSoft_User createdBy
            on E.CreatedBy = createdBy.ID
        Left join LKACSoft_ProcessSchemaStatus processSchemaStatus
            on E.ProcessSchemaStatus = processSchemaStatus.ProcessSchemaStatusID
        Left join LKACSoft_ProcessStatus processStatus
            on processSchemaStatus.ProcessStatus = processStatus.ProcessStatusID
        Left join LKACSoft_ProcessSchema processSchema
            on E.ProcessSchemaID = processSchema.ProcessSchemaID
        Left join LKACSoft_Customer relatedToCustomer
            on E.RelatedToCustomer = relatedToCustomer.Code
        Left join LKACSoft_ExecutionAttributesValue ExecutionAttributesValue
            on E.ExecutionID = ExecutionAttributesValue.ExecutionID
        Left join LKACSoft_ExecutionAttributesDefinition ExecutionAttributesDefinition
            on ExecutionAttributesValue.FieldID = ExecutionAttributesDefinition.FieldID
    --Where T.RelatedToProcess = P.ProcessID
    GROUP BY 
        E.ExecutionID, 
        E.ExecutionName, 
        E.CreatedBy,
        E.DateCreated,
        E.IsPeriodic,

        -- Columns from LKACSoft_ExecutionAttributesDefinition (FieldName)
        ExecutionAttributesDefinition.FieldName,

        -- Columns from LKACSoft_ExecutionAttributesValue (FieldValue)
        ExecutionAttributesValue.FieldValue,
        
        createdBy.ID,
        createdBy.Username,
        createdBy.Firstname,
        createdBy.Lastname,
        createdBy.Avatar,
        createdBy.Address,
        createdBy.District,
        createdBy.Dob,
        createdBy.IsQuitJob,
        createdBy.DateCreate,
        createdBy.Team,

        processSchemaStatus.ProcessSchemaStatusID,
        processSchemaStatus.ProcessSchema,
        processSchemaStatus.OrderIndex,
        processSchemaStatus.CreatedAt,

        -- Columns from LKACSoft_ProcessStatus
        processStatus.ProcessStatusID,
        processStatus.StatusName,
        processStatus.DesignatedColor,

        processSchema.ProcessSchemaID,
        processSchema.Name,
        processSchema.Description,
        processSchema.CreatedAt,
        processSchema.UpdatedAt,
        
        -- Add all relevant columns from the relatedToCustomer table
        relatedToCustomer.Code,
        relatedToCustomer.Name,
        relatedToCustomer.ShortName,
        relatedToCustomer.Address,
        relatedToCustomer.LogoS3Key,
        relatedToCustomer.FilterLocation,
        relatedToCustomer.GetDocsDate,
        relatedToCustomer.DateCreate,
        relatedToCustomer.Suspended,
        relatedToCustomer.SuspendedTo,
        relatedToCustomer.Dissolved,
        relatedToCustomer.DissolvedDate,
        relatedToCustomer.MainAccountant,
        relatedToCustomer.CreatedBy,
        relatedToCustomer.AssignedToCustomerSupport,
        relatedToCustomer.ResponsibleAccountantTeam,
        relatedToCustomer.LKACSoft_DepartmentCode,
        relatedToCustomer.ContractExpiry,
        relatedToCustomer.ContractSignedDate;
go

-- SELECT OBJECT_TYPE = o.type_desc
-- FROM sys.objects o
-- WHERE o.name = 'V_DetailCustomers';