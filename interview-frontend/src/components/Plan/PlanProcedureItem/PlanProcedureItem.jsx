import React, { useState, useEffect } from "react";
import ReactSelect from "react-select"; 
import { useParams } from "react-router-dom";
import {
    addOrRemoveUsers
} from "../../../api/api";

const PlanProcedureItem = ({ procedure, users ,assignedUsers}) => {
    const [selectedUsers, setSelectedUsers] = useState([]);
    let { id } = useParams();

    useEffect(() => { 
         if(assignedUsers && assignedUsers.length > 0) {
                var userOptions = []; 
                assignedUsers.map((u) => userOptions.push({ label: users.find((e)=>e.value == u.userId).label, value: u.userId }));
                 setSelectedUsers(userOptions) 
       }
      },[id]);
    const handleAssignUserToProcedure = async (e) => {
 
         const deletedUser =   selectedUsers.filter(userItem =>
             !e.some(selectedUserItem =>  userItem.value === selectedUserItem.value))  
        const assigneduser =  e.filter(userItem => !selectedUsers.some(selectedUserItem =>  userItem.value === selectedUserItem.value ))  
        setSelectedUsers(e); 
        if(assigneduser.length > 0)  await addOrRemoveUsers(id,procedure.procedureId,assigneduser[0].value);
        if(deletedUser.length > 0)   await addOrRemoveUsers(id,procedure.procedureId,deletedUser[0].value);  
   
     
    };

    return (
        <div className="py-2">
            <div>
                {procedure.procedureTitle}
            </div>

            <ReactSelect
                className="mt-2"
                placeholder="Select User to Assign"
                isMulti={true}
                options={users}
                value={selectedUsers}
                onChange={(e) => handleAssignUserToProcedure(e)}
            />
        </div>
    );
};

export default PlanProcedureItem;
