/**
 * Returns date string of the Date object.
 */
export function getDate(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, "0");
    const day = date
        .getDate()
        .toString()
        .padStart(2, "0");
    return `${year}-${month}-${day}`;
}

/**
 * Returns time string of the Date object.
 */
export function getTime(date: Date): string {
    const hours = date
        .getHours()
        .toString()
        .padStart(2, "0");
    const minutes = date
        .getMinutes()
        .toString()
        .padStart(2, "0");
    return `${hours}:${minutes}`;
}

/**
 * Returns human-readeable string representation of the Date object.
 */
export function formatDateTime(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, "0");
    const day = date
        .getDate()
        .toString()
        .padStart(2, "0");
    const hours = date
        .getHours()
        .toString()
        .padStart(2, "0");
    const minutes = date
        .getMinutes()
        .toString()
        .padStart(2, "0");
    return `${day}.${month}.${year} ${hours}:${minutes}`;
}

/**
 * Updated date part of the Date object.
 */
export function setDate(date: Date, newDateStr: string): Date {
    const matches = newDateStr.match(/^(\d+).(\d+).(\d+)/);
    if (!matches) {
        return date;
    }

    date.setFullYear(+matches[1]);
    date.setMonth(+matches[2] - 1);
    date.setDate(+matches[3]);

    return date;
}

/**
 * Updated time part of the Date object.
 */
export function setTime(date: Date, newTimeStr: string): Date {
    const matches = newTimeStr.match(/^(\d+).(\d+)/);
    if (!matches) {
        return date;
    }

    date.setHours(+matches[1]);
    date.setMinutes(+matches[2]);

    return date;
}
