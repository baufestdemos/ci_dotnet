import { TodoTask } from "@/domain/TodoTask"
import React, { useState } from "react"

type TodoTaskProps = {
    todoTask: TodoTask,
    removeAction: (id: number) => void,
    editAction: (todoTask: TodoTask) => void,
    children?: React.ReactElement
}

export function TodoTaskItem({ ...props }: TodoTaskProps) {
    const [visibleDescription, setVisibleDescription] = useState(false);
    const [description, setDescription] = useState(props.todoTask.description);

    return (
        <div style={{ borderBottom: '1px solid lightgray', marginTop: '20px', paddingBottom: '5px' }}>
            {props.todoTask.subject}
            <button type="button" className="btn-sm btn-red" style={{ float: 'right' }} onClick={() => props.removeAction(props.todoTask.id)}>Quitar</button>
            <button type="button" className="btn-sm btn-green" style={{ float: 'right', marginRight: '5px' }} onClick={() => setVisibleDescription(true)}>Desc</button>
            {
                visibleDescription ?
                    <>
                        <div>
                            <textarea maxLength={250} rows={4} style={{ width: '100%', marginTop: '5px' }}
                                className="shadow appearance-none border rounded text-gray-700 leading-tight focus:outline-none focus:shadow-outline" onChange={(e) => setDescription(e.target.value)}>
                                {description}
                            </textarea>
                        </div>
                        <div className="flex justify-end" >
                            <button type="button" className="btn-sm btn-blue" onClick={() => {
                                setVisibleDescription(false);
                                props.todoTask.description = description;
                                props.editAction(props.todoTask);
                            }}>Guardar</button>
                        </div>
                    </> : <></>
            }
        </div>
    )
}