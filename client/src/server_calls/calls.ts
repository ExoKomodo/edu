export const calls = {
    get: async () => {
        // TODO - edit endpoints and fetch data
        const response = await fetch(`http://localhost:8000/api/blog`,
        {
            method: 'GET'
        });
        
        if (!response.ok){
            throw new Error('Failed to fetch data from the server')
        }
        
        return await response.json()
    },

    post: async(data: any = {}) => {
        const response = await fetch('http://localhost:8000/api/blog',
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        })

        if (!response.ok) {
            throw new Error('Could not post data')
        }

        return await response.json()
    },

    update: async (id:string, data: any = {}) => {
        const response = await fetch(`http://localhost:8000/api/blog/${id}`,
        {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify(data)
        })

        if (!response.ok){
            throw new Error('Failed to update data on server')
        }

        return await response.json()
    },
    delete: async (id:string) => {
        const response = await fetch(`http://localhost:8000/api/blog/${id}`,
        {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json',
            }
        })

        if (!response.ok){
            throw new Error('Failed to delete data on server')
        }

        return;
    },
}