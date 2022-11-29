/* tslint:disable */
/* eslint-disable */
/**
 * ElderlyCare.Api
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { exists, mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface FloorPlan
 */
export interface FloorPlan {
    /**
     * 
     * @type {string}
     * @memberof FloorPlan
     */
    id?: string;
    /**
     * 
     * @type {string}
     * @memberof FloorPlan
     */
    name?: string | null;
    /**
     * 
     * @type {string}
     * @memberof FloorPlan
     */
    svgUrl?: string | null;
}

/**
 * Check if a given object implements the FloorPlan interface.
 */
export function instanceOfFloorPlan(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function FloorPlanFromJSON(json: any): FloorPlan {
    return FloorPlanFromJSONTyped(json, false);
}

export function FloorPlanFromJSONTyped(json: any, ignoreDiscriminator: boolean): FloorPlan {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'id': !exists(json, 'id') ? undefined : json['id'],
        'name': !exists(json, 'name') ? undefined : json['name'],
        'svgUrl': !exists(json, 'svgUrl') ? undefined : json['svgUrl'],
    };
}

export function FloorPlanToJSON(value?: FloorPlan | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'id': value.id,
        'name': value.name,
        'svgUrl': value.svgUrl,
    };
}
