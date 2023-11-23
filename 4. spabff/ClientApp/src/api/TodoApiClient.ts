import { Result } from "@/domain/Result";
import { TodoTask } from "@/domain/TodoTask";
import { TodoTaskDescription } from "@/domain/TodoTaskDescription";
import HttpClient from "@/utils/HttpClient";

export class TodoApiClient extends HttpClient {
    constructor() {
        super({
            url: '/api'
        });
    }

    public async all(): Promise<TodoTask[]> {
        return (await this.get('/task')).data;
    }

    public async add(data: TodoTask): Promise<Result<TodoTask>> {
        return (await this.post('/task', data)).data;
    }

    public async remove<Key>(id: Key): Promise<Result<TodoTask>> {
        return (await this.delete(`/task?id=${id}`)).data;
    }

    public async editDescription(data: TodoTaskDescription): Promise<Result<TodoTaskDescription>> {
        return (await this.put('/task/description/edit', data)).data;
    }
}