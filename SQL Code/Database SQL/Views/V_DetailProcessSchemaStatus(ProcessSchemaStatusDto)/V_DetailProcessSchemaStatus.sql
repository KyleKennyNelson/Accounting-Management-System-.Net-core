use db_ab6e43_lkacsoftdb
go

--drop view V_DetailProcessSchemaStatuses

Create or Alter View V_DetailProcessSchemaStatuses
AS
    Select
        -- Columns from LKACSoft_ProcessSchemaStatus (Main Table)
        PSS.*,

        -- Columns from LkacSoft_ProcessSchema (ProcessSchema Table)
        PSch.ProcessSchemaID,
        Psch.Name as ProcessSchemaName,
        Psch.Description as ProcessSchemaDescription,
        Psch.CreatedAt as ProcessSchemaCreatedAt,
        Psch.UpdatedAt as ProcessSchemaUpdatedAt,

        -- Columns from LkacSoft_ProcessStatus (ProcessStatus Table)
        PSta.*

    FROM LKACSoft_ProcessSchemaStatus PSS

        LEFT JOIN LKACSoft_ProcessSchema PSch
            on PSS.ProcessSchema = PSch.ProcessSchemaID

        LEFT JOIN LKACSoft_ProcessStatus PSta
            on PSS.ProcessStatus = PSta.ProcessStatusID
go