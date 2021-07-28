import 'package:json_annotation/json_annotation.dart';

enum SajdahType {
  @JsonValue('swaggerGeneratedUnknown')
  swaggerGeneratedUnknown,
  @JsonValue('None')
  none,
  @JsonValue('Place')
  place,
  @JsonValue('Reason')
  reason,
  @JsonValue('PlaceAndReason')
  placeandreason
}

const $SajdahTypeMap = {
  SajdahType.none: 'None',
  SajdahType.place: 'Place',
  SajdahType.reason: 'Reason',
  SajdahType.placeandreason: 'PlaceAndReason',
  SajdahType.swaggerGeneratedUnknown: ''
};

enum PlaceOfRevelationType {
  @JsonValue('swaggerGeneratedUnknown')
  swaggerGeneratedUnknown,
  @JsonValue('Makie')
  makie,
  @JsonValue('Madani')
  madani
}

const $PlaceOfRevelationTypeMap = {
  PlaceOfRevelationType.makie: 'Makie',
  PlaceOfRevelationType.madani: 'Madani',
  PlaceOfRevelationType.swaggerGeneratedUnknown: ''
};
