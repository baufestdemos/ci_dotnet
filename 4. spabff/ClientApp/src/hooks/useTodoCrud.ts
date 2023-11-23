import { TodoTask } from "@/domain/TodoTask"
import { useState, useCallback } from "react";
import HttpClient from "@/utils/HttpClient";
import { TodoApiClient } from "@/api/TodoApiClient";

export const useTodoCrud = () => {
    const [todoList, setTodoList] = useState<TodoTask[]>([])
    const todoApiClient = HttpClient.instance(TodoApiClient);

    const remoteList = useCallback(() => {
        todoApiClient.all().then(todoList => {
            setTodoList(todoList);
        });
    }, [todoApiClient]);

    const addNewTask = (subject?: string) => {
        if (subject) {
            todoApiClient.add({
                id: 0,
                subject: subject
            }).then(result => {
                if (result.success) {
                    remoteList();
                }
            })

        }
    }

    const removeTask = (id: number) => {
        todoApiClient.remove(id)
            .then(result => {
                if (result.success) {
                    remoteList();
                }
            });
    }

    const editTask = (todoTask: TodoTask) => {
        if (todoTask.description) {
            todoApiClient.editDescription({
                taskId: todoTask.id,
                description: todoTask.description
            }).then(result => {
                if (result.success) {
                    remoteList();
                }
            })
        }
    }

    return {
        todoList,
        remoteList,
        addNewTask,
        removeTask,
        editTask
    }
}