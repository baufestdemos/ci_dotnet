import { TodoInput } from "./TodoInput"
import { TodoTaskItem } from "./TodoTaskItem"
import { useEffect } from "react";
import { useTodoCrud } from "@/hooks/useTodoCrud";

export function TodoList() {
    const { todoList, remoteList, removeTask, addNewTask, editTask } = useTodoCrud();

    useEffect(() => {
        remoteList();
    }, [remoteList]);

    return (
        <>
            <TodoInput onAddAction={addNewTask} />
            {todoList.map(todoTask => (
                <TodoTaskItem todoTask={todoTask} key={todoTask.id} removeAction={removeTask} editAction={editTask} />
            ))}
        </>
    )
}