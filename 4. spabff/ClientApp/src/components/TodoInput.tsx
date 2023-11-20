import { useState } from "react";

type TodoInputProps = {
    onAddAction: (taskSubject?: string) => void
}

export function TodoInput({ ...props }: TodoInputProps) {
    const [subject, setSubject] = useState<string | undefined>();
    return (
        <div>
            <input type="text" maxLength={150}
                className="shadow appearance-none border rounded py-2 px-10 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                onChange={(e) => { setSubject(e.target.value); }} />
            <button type="button" className="btn btn-blue" onClick={() => props.onAddAction(subject)}>Agregar</button>
        </div>
    )
}