import { Blog } from "@/models";

export const apiMapper = ( apiData: any ) : Blog => {

    if ( apiData instanceof Blog ) {
        return apiData;
    }
    throw new Error("API Data not of type Blog");
}