import { TodoTask } from "@/domain/TodoTask"
import React from "react"

type TodoTaskProps = {
    todoTask: TodoTask,
    removeAction: (id: number) => void,
    children?: React.ReactElement
}

export function TodoTaskItem({ ...props }: TodoTaskProps) {
    return (
        <div style={{ borderBottom: '1px solid lightgray', marginTop: '20px', paddingBottom: '5px' }}>
            {props.todoTask.subject}
            <button type="button" className="btn-sm btn-red" style={{ float: 'right' }} onClick={() => props.removeAction(props.todoTask.id)}>Quitar</button>
        </div>
    )
}