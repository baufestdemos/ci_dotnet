import { TodoTask } from "@/domain/TodoTask"
import { TodoInput } from "./TodoInput"
import { TodoTaskItem } from "./TodoTaskItem"
import { useState } from "react";

const data: TodoTask[] = [
    { id: 1, subject: 'hola 1' },
    { id: 2, subject: 'hola 2' }
]

export function TodoList() {
    const [todoList, setTodoList] = useState(data)

    const addNewTask = (subject?: string) => {
        if (subject) {
            setTodoList([
                ...todoList,
                { id: todoList.length + 1, subject }
            ]);
        }
    }

    const removeTask = (id: number) => {
        setTodoList(
            todoList.filter(a =>
                a.id !== id
            )
        );
    }

    return (
        <>
            <TodoInput onAddAction={addNewTask} />
            {todoList.map(todoTask => (
                <TodoTaskItem todoTask={todoTask} key={todoTask.id} removeAction={removeTask} />
            ))}
        </>
    )
}