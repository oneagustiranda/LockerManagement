export interface Locker {
    lockerNo: string;
    employeeNumber: string | null;    
    size: number;
    location: string;
    isEmpty: boolean;
}