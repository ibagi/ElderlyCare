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
 * @interface FloorPlanModel
 */
export interface FloorPlanModel {
    /**
     * 
     * @type {string}
     * @memberof FloorPlanModel
     */
    id?: string;
    /**
     * 
     * @type {string}
     * @memberof FloorPlanModel
     */
    name?: string | null;
    /**
     * 
     * @type {string}
     * @memberof FloorPlanModel
     */
    svgUrl?: string | null;
}

/**
 * Check if a given object implements the FloorPlanModel interface.
 */
export function instanceOfFloorPlanModel(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function FloorPlanModelFromJSON(json: any): FloorPlanModel {
    return FloorPlanModelFromJSONTyped(json, false);
}

export function FloorPlanModelFromJSONTyped(json: any, ignoreDiscriminator: boolean): FloorPlanModel {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'id': !exists(json, 'id') ? undefined : json['id'],
        'name': !exists(json, 'name') ? undefined : json['name'],
        'svgUrl': !exists(json, 'svgUrl') ? undefined : json['svgUrl'],
    };
}

export function FloorPlanModelToJSON(value?: FloorPlanModel | null): any {
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

