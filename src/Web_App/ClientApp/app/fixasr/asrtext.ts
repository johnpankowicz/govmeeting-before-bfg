import { AsrSegment } from './asrsegment';

// This is the JSON format of the text to be fixed.

export class AsrText {
    lastedit: number;
    asrsegments: AsrSegment[];
}
